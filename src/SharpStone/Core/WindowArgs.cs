namespace SharpStone.Core;

public struct WindowArgs
{
    public string Title { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Resizable { get; set; }
    public bool AlwaysOnTop { get; set; }
}
