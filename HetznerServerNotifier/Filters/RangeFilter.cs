using System.Numerics;
using HetznerServerNotifier.Models;
using Microsoft.Extensions.Options;

namespace HetznerServerNotifier;

public class RangeFilter<T> : IFilter where T : INumber<T>
{
	private readonly Func<FilterConfiguration, Range<T>> _configAccessor;
	private readonly IOptionsMonitor<FilterConfiguration> _configuration;
	private readonly Func<ServerInfo, T> _serverAccessor;

	public RangeFilter(IOptionsMonitor<FilterConfiguration> configuration, Func<FilterConfiguration, Range<T>> configAccessor,
		Func<ServerInfo, T> serverAccessor)
	{
		_configuration = configuration;
		_configAccessor = configAccessor;
		_serverAccessor = serverAccessor;
	}

	public bool Execute(ServerInfo serverInfo)
	{
		var settings = _configAccessor(_configuration.CurrentValue);
		var value = _serverAccessor(serverInfo);

		if (value < settings.Min)
			return false;

		if ((settings.Max > T.Zero) && (value > settings.Max))
			return false;

		return true;
	}
}
