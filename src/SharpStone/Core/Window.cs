using SharpStone.Windows;
using static SharpStone.GlobalConfiguration;

namespace SharpStone.Core;

public struct WindowArgs(string title, int width = 1280, int height = 720)
{
    public string Title { get; set; } = title;
    public int Width { get; set; } = width;
    public int Height { get; set; } = height;
    public bool Resizable { get; set; } = false;
    public bool AlwaysOnTop { get; set; } = false;
}

public static class Window
{
    private static IWindow _instance;

    public static string Title =>
        _instance.Title;

    public static int Width =>
        _instance.Width;

    public static int Height =>
        _instance.Height;

    public static bool Fullscreen =>
        _instance.Fullscreen;

    public static void Init(WindowArgs args)
    {
        var window = Os switch
        {
            OperatingSystem.Windows => new SDL2Window(),
            _ => throw new NotSupportedException("OS not supported."),
        };

        if (!window.Init(args))
        {
            Logger.Error<IWindow>($"Failed to initialize a window.");
        }

        _instance = window;
    }

    public static bool Shutdown()
        => _instance.Shutdown();

    public static void Update()
        => _instance.Update();
}

public interface IWindow
{
    string Title { get; }
    int Width { get; }
    int Height { get; }
    bool Fullscreen { get; }

    bool Init(WindowArgs args);
    bool Shutdown();
    void Update();
}
