namespace SharpStone.Core;

public struct WindowArgs(string title)
{
    public string Title { get; set; } = title;
    public int Width { get; set; } = 1280;
    public int Height { get; set; } = 720;
    public bool Resizable { get; set; } = false;
    public bool AlwaysOnTop { get; set; } = false;
}
