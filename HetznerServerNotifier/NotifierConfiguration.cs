namespace HetznerServerNotifier;

public record NotifierConfiguration
{
	public required ushort DelayInSeconds { get; init; } = 60;
	public required IReadOnlyList<long> UsersToNotify { get; init; }
}
