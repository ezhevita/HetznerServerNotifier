using System.Net.Http.Json;
using System.Text;
using HetznerServerNotifier.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace HetznerServerNotifier;

public class NotifierService : BackgroundService
{
	public const string HttpClientName = "Hetzner";
	private readonly ITelegramBotClient _botClient;
	private readonly ILogger<NotifierService> _logger;
	private readonly IOptionsMonitor<NotifierConfiguration> _config;
	private readonly IHttpClientFactory _factory;
	private readonly IEnumerable<IFilter> _filters;
	private readonly HashSet<ServerInfo> _previousServers = [];

	public NotifierService(IOptionsMonitor<NotifierConfiguration> config, IHttpClientFactory factory,
		IEnumerable<IFilter> filters, ITelegramBotClient botClient, ILogger<NotifierService> logger)
	{
		_config = config;
		_factory = factory;
		_filters = filters;
		_botClient = botClient;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (true)
		{
			await Run();

			try
			{
				await Task.Delay(TimeSpan.FromSeconds(_config.CurrentValue.DelayInSeconds), stoppingToken);
			} catch (TaskCanceledException)
			{
				return;
			}

			if (stoppingToken.IsCancellationRequested)
				return;
		}
	}

	private async Task Run()
	{
		var httpClient = _factory.CreateClient(HttpClientName);
		var data = await httpClient.GetFromJsonAsync<ServerData>(
			new Uri("https://www.hetzner.com/_resources/app/jsondata/live_data_en.json"));

		if (data == null)
		{
			LoggerMethods.ErrorRetrievingData(_logger);
			return;
		}

		var servers = _filters.Aggregate<IFilter, IEnumerable<ServerInfo>>(
				data.Servers, (current, filter) => current.Where(filter.Execute))
			.Except(_previousServers)
			.ToList();

		if (servers.Count > 0)
		{
			_previousServers.UnionWith(servers);
			var builder = new StringBuilder("<b>New servers found!</b>");
			builder.AppendLine();

			foreach (var server in servers)
			{
				builder.AppendLine(server.ToString());
				builder.AppendLine();
			}

			var message = builder.ToString();

			foreach (var user in _config.CurrentValue.UsersToNotify)
			{
				await _botClient.SendTextMessageAsync(user, message, parseMode: ParseMode.Html);
			}
		}
	}
}
