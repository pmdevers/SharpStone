using System.Numerics;

namespace SharpStone.Gui;

public enum Corner
{
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight,
    Bottom,
    Top,
    Fill,
    Center
}

public abstract class BaseControl : IDisposable
{
    protected BaseControl()
    { 
        Name = string.Empty;
        Position = Vector2.Zero;
        Size = Vector2.Zero;
        Visible = true;
        //RelativeTo = Corner.Fill;
    }

    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    //public Corner RelativeTo { get; set; }
    public bool Visible { get; set; }
    public BaseControl? Parent { get; set; }

    public abstract void Draw();

    public abstract void Update();

    public void Dispose()
    {
        Disposing(true);
        GC.SuppressFinalize(this);
    }

    protected abstract void Disposing(bool disposing);
}