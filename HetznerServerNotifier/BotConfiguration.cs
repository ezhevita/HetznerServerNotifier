namespace HetznerServerNotifier;

public record BotConfiguration
{
	public required string Token { get; init; }
}
