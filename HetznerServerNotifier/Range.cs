using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HetznerServerNotifier;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public record Range<T> where T : INumber<T>
{
	public T Min { get; init; } = T.Zero;
	public T Max { get; init; } = T.Zero;
}
