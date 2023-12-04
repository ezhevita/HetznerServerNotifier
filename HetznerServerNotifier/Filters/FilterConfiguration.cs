namespace HetznerServerNotifier;

public record FilterConfiguration
{
	public HashSet<string> Location { get; init; } = [];
	public Range<byte> Cores { get; init; } = new();
	public Range<decimal> Price { get; init; } = new();
	public Range<float> Frequency { get; init; } = new();
	public Range<ushort> Ram { get; init; } = new();
}
