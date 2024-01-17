using SharpStone.Core;

namespace SharpStone;

public static class Logging
{
    public static ILogger Logger = new ConsoleLogger();
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
