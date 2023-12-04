using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HetznerServerNotifier.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public record Datacenter
{
	[JsonPropertyName("shortname")]
	public required string ShortName { get; init; }

	public required string Country { get; init; }
}
