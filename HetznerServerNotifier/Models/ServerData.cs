using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HetznerServerNotifier.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public record ServerData
{
	[JsonPropertyName("server")]
	public required IReadOnlyList<ServerInfo> Servers { get; init; }
}
