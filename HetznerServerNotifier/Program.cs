using System.Net.Http.Headers;
using HetznerServerNotifier;
using Microsoft.Extensions.Options;
using Telegram.Bot;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
services.Configure<BotConfiguration>(builder.Configuration.GetSection("Bot"));
services.Configure<NotifierConfiguration>(builder.Configuration.GetSection("Notifier"));
services.Configure<FilterConfiguration>(builder.Configuration.GetSection("Filters"));

services.AddRangeFilter(config => config.Price, server => server.Price)
	.AddRangeFilter(config => config.Cores, server => server.Cores)
	.AddRangeFilter(config => config.Frequency, server => server.Frequency)
	.AddRangeFilter(config => config.Ram, server => server.Ram)
	.AddListFilter(config => config.Location, server => server.Datacenters.First().ShortName);

services.AddSingleton<ITelegramBotClient, TelegramBotClient>(
	sp =>
		new TelegramBotClient(new TelegramBotClientOptions(sp.GetRequiredService<IOptions<BotConfiguration>>().Value.Token)));

services.AddHttpClient(
	NotifierService.HttpClientName,
	client => client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(HetznerServerNotifier), "1.0")));

services.AddHostedService<NotifierService>();

var host = builder.Build();
await host.RunAsync();
