namespace HetznerServerNotifier;

public static partial class LoggerMethods
{
	[LoggerMessage(Level = LogLevel.Error, EventId = 10000, Message = "Could not retrieve servers data!")]
	public static partial void ErrorRetrievingData(ILogger logger);
}
