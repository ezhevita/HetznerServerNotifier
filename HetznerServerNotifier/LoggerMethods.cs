namespace HetznerServerNotifier;

public static partial class LoggerMethods
{
	[LoggerMessage(Level = LogLevel.Error, EventId = 10000, Message = "Could not retrieve servers data!")]
	public static partial void ErrorRetrievingData(this ILogger logger);

	[LoggerMessage(Level = LogLevel.Error, EventId = 10001, Message = "An exception has occurred!")]
	public static partial void ExceptionOccurred(this ILogger logger, Exception exception);
}
