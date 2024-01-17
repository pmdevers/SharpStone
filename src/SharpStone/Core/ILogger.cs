namespace SharpStone.Core;

public enum LogLevel
{
    Info,
    Debug,
    Warning,
    Error,
    Fatal,
}

public interface ILogger
{
    void Log(LogLevel level, string name, string message);
}

public static class ILoggerExtensions
{
    public static void Assert<T>(this ILogger logger, bool assert, string message)
    {
#if DEBUG
        if (!assert)
        {
            logger.Warning<T>(message);
        }
#endif
    }

    public static void Trace<T>(this ILogger logger, string message)
    {
#if DEBUG
        logger.Info<T>(message);
#endif
    }
    public static void Info<T>(this ILogger logger, string message)
        => logger.Log<T>(LogLevel.Info, message);
    public static void Error<T>(this ILogger logger, string message)
        => logger.Log<T>(LogLevel.Error, message);
    public static void Debug<T>(this ILogger logger, string message)
        => logger.Log<T>(LogLevel.Debug, message);
    public static void Warning<T>(this ILogger logger, string message)
        => logger.Log<T>(LogLevel.Warning, message);
    public static void Fatal<T>(this ILogger logger, string message)
        => logger.Log<T>(LogLevel.Fatal, message);

    public static void Log<T>(this ILogger logger, LogLevel level, string message)
        => logger.Log(level, typeof(T).Name, message);
}
