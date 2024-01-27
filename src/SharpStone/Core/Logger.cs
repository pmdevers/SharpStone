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

public static class Logger
{
    private static readonly ILogger _instance = new ConsoleLogger();

    public static void Log(LogLevel level, string name, string message)
    {
        _instance.Log(level, name, message);
    }
    public static void Log<T>(this ILogger logger, LogLevel level, string message)
        => logger.Log(level, typeof(T).Name, message);

    public static void Assert<T>(bool assert, string message)
    {
#if DEBUG
        if (!assert)
        {
            Warning<T>(message);
        }
#endif
    }

    public static void Trace<T>(string message)
    {
#if DEBUG
        Info<T>(message);
#endif
    }
    public static void Info<T>(string message)
        => _instance.Log<T>(LogLevel.Info, message);
    public static void Error<T>(string message)
        => _instance.Log<T>(LogLevel.Error, message);
    public static void Debug<T>(string message)
        => _instance.Log<T>(LogLevel.Debug, message);
    public static void Warning<T>(string message)
        => _instance.Log<T>(LogLevel.Warning, message);
    public static void Fatal<T>(string message)
        => _instance.Log<T>(LogLevel.Fatal, message);
    public static void Log<T>(LogLevel level, string message)
        => _instance.Log(level, typeof(T).Name, message);
}

internal class ConsoleLogger : ILogger
{
    public void Log(LogLevel level, string name, string message)
    {
        var color = GetLogLevelColors(level);
        var msg = FormatMessage(level, name, message);
        WriteWithColor(msg, true, color);
    }
    private string FormatMessage(LogLevel level, string name, string message)
    {
        var abbr = GetLogLevelString(level);
        return $"[{DateTime.Now}] {name} {abbr}: {message}";
    }

    private static string GetLogLevelString(LogLevel level)
    {
        return level switch
        {
            LogLevel.Info => "INFO",
            LogLevel.Debug => "DBUG",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "ERRR",
            LogLevel.Fatal => "FATL",
            _ => "",
        };
    }

    private static void WriteWithColor(string message, bool newline, ConsoleColors colors)
    {
        Console.BackgroundColor = colors.Background;
        Console.ForegroundColor = colors.Forground;

        if (newline)
        {
            Console.WriteLine(message);
        }
        else
        {
            Console.Write(message, colors.Background, colors.Forground);
        }
        Console.ResetColor();
    }

    private static ConsoleColors GetLogLevelColors(LogLevel level)
    {
        return level switch
        {
            LogLevel.Info => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Blue),
            LogLevel.Debug => new ConsoleColors(ConsoleColor.Black, ConsoleColor.White),
            LogLevel.Warning => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Yellow),
            LogLevel.Error => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red),
            LogLevel.Fatal => new ConsoleColors(ConsoleColor.Red, ConsoleColor.White),
            _ => new ConsoleColors(ConsoleColor.Black, ConsoleColor.White),
        };
    }

    public readonly struct ConsoleColors(ConsoleColor background, ConsoleColor foreground)
    {
        public ConsoleColor Background { get; } = background;
        public ConsoleColor Forground { get; } = foreground;
    }
}
