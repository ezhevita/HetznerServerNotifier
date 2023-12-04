using HetznerServerNotifier.Models;
using Microsoft.Extensions.Options;

namespace HetznerServerNotifier;

public class ListFilter<T> : IFilter
{
	private readonly Func<FilterConfiguration, ISet<T>> _configAccessor;
	private readonly IOptionsMonitor<FilterConfiguration> _configuration;
	private readonly Func<ServerInfo, T> _serverAccessor;

	public ListFilter(IOptionsMonitor<FilterConfiguration> configuration,
		Func<FilterConfiguration, ISet<T>> configAccessor, Func<ServerInfo, T> serverAccessor)
	{
		_configuration = configuration;
		_configAccessor = configAccessor;
		_serverAccessor = serverAccessor;
	}

	public bool Execute(ServerInfo serverInfo)
	{
		var values = _configAccessor(_configuration.CurrentValue);

		return (values.Count < 1) || values.Contains(_serverAccessor(serverInfo));
	}
}
