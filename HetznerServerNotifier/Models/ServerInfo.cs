using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace HetznerServerNotifier.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public record ServerInfo
{
	public required string Name { get; init; }
	public required byte Cores { get; init; }
	public required byte Threads { get; init; }
	public required string Cpu { get; init; }
	public required float Frequency { get; init; }
	public required ushort Ram { get; init; }

	[JsonPropertyName("ram_hr")]
	public required string RamDescription { get; init; } = null!;

	public required decimal Price { get; init; }

	[JsonPropertyName("setup_price")]
	public required decimal SetupPrice { get; init; }

	[JsonPropertyName("hdd_arr")]
	[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
	public required ValueEqualityList<string> Disks { get; init; }

	[JsonPropertyName("datacenter")]
	public required ValueEqualityList<Datacenter> Datacenters { get; init; } = null!;

	public override string ToString()
	{
		var builder = new StringBuilder();
		builder.AppendLine(Name);
		builder.AppendLine(CultureInfo.InvariantCulture, $"<b>CPU</b>: {Cpu} ({Cores}c/{Threads}t, {Frequency:#.#} GHz)");
		builder.AppendLine(CultureInfo.InvariantCulture, $"<b>RAM</b>: {RamDescription}");

		var disksFormatted = Disks.GroupBy(x => x).Select(x => $"{x.Count()}x {x.Key}");
		builder.Append("<b>Disks</b>: ");
		builder.AppendJoin(", ", disksFormatted);
		builder.AppendLine();
		builder.AppendLine();
		builder.AppendLine(
			CultureInfo.InvariantCulture, $"<b>Location</b>: {string.Join(", ", Datacenters.Select(x => x.Country))}");
		builder.AppendLine(CultureInfo.InvariantCulture, $"<b>Price</b>: {Price}\u20ac/mo, setup fee {SetupPrice}\u20ac");

		return builder.ToString();
	}
}
