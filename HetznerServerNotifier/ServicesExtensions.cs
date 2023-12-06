using System.Numerics;
using HetznerServerNotifier.Models;
using Microsoft.Extensions.Options;

namespace HetznerServerNotifier;

public static class ServicesExtensions
{
	public static IServiceCollection AddListFilter<T>(this IServiceCollection serviceCollection,
		Func<FilterConfiguration, ISet<T>> configAccessor, Func<ServerInfo, IList<T>> serverAccessor)
	{
		serviceCollection.AddSingleton<IFilter, ListFilter<T>>(
			sp => new ListFilter<T>(
				sp.GetRequiredService<IOptionsMonitor<FilterConfiguration>>(), configAccessor, serverAccessor));

		return serviceCollection;
	}

	public static IServiceCollection AddRangeFilter<T>(this IServiceCollection serviceCollection,
		Func<FilterConfiguration, Range<T>> configAccessor, Func<ServerInfo, T> serverAccessor) where T : INumber<T>
	{
		serviceCollection.AddSingleton<IFilter, RangeFilter<T>>(
			sp => new RangeFilter<T>(
				sp.GetRequiredService<IOptionsMonitor<FilterConfiguration>>(), configAccessor, serverAccessor));

		return serviceCollection;
	}
}
