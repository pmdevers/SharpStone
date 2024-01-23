using SharpStone.Core;
using SharpStone.Gui.Controls;
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
    //CenterLeft,
    //CenterRight,
    Fill,
    Center
}

public readonly struct Tranforms
{
    public static Matrix4x4 TopLeftCorner = Matrix4x4.CreateTranslation(new Vector3(1, -1, 0));
    public static Matrix4x4 TopCorner = Matrix4x4.CreateTranslation(new Vector3(0, -1, 0));
    public static Matrix4x4 TopRightCorner = Matrix4x4.CreateTranslation(new Vector3(-1, 1, 0));
    public static Matrix4x4 RightCorner = Matrix4x4.CreateTranslation(new Vector3(-1, 0, 0));
    public static Matrix4x4 BottomRightCorner = Matrix4x4.CreateTranslation(new Vector3(-1, 1, 0));
    public static Matrix4x4 BottomCorner = Matrix4x4.CreateTranslation(new Vector3(0, 1, 0));
    public static Matrix4x4 LeftBottomCorner = Matrix4x4.CreateTranslation(new Vector3(1, 1, 0));
    public static Matrix4x4 LeftCorner = Matrix4x4.CreateTranslation(new Vector3(1, 0, 0));
    public static Matrix4x4 Center = Matrix4x4.CreateTranslation(new Vector3(0, 0, 0));
}


public abstract class BaseControl : IDisposable
{
    protected BaseControl()
    { 
        Name = string.Empty;
        Position = Vector2.Zero;
        Size = Vector2.Zero;
        Visible = true;
        RelativeTo = Corner.TopLeft;
        Transparency = 100;
    }

    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Corner RelativeTo { get;set; }
    public Vector2 Size { get; set; }
    //public Corner RelativeTo { get; set; }
    public bool Visible { get; set; }
    public int Transparency { get; set; }

    public BaseControl? Parent { get; set; }

    public ControlCollection Controls => new(this);

    public abstract void Draw();

    public abstract void Update();

    public CornerPosition Corners => new(Position, Size);
    internal Vector2 CorrectedPosition { get; private set; }

    public void Dispose()
    {
        Disposing(true);
        GC.SuppressFinalize(this);
    }

    protected abstract void Disposing(bool disposing);

    public virtual void OnResize() 
    {
        if (Parent == null)
        {
            if (RelativeTo == Corner.BottomLeft) CorrectedPosition = Position;
            else if (RelativeTo == Corner.TopLeft)
                CorrectedPosition = new Vector2(Position.X, Position.Y);
            else if (RelativeTo == Corner.BottomRight)
                CorrectedPosition = new Vector2(UserInterface.Width - Position.X - Size.X, Position.Y);
            else if (RelativeTo == Corner.TopRight)
                CorrectedPosition = new Vector2(UserInterface.Width - Position.X - Size.X, -Position.Y - Size.Y);
            else if (RelativeTo == Corner.Bottom)
                CorrectedPosition = new Vector2(UserInterface.Width / 2 - Size.X / 2 + Position.X, Position.Y);
            else if (RelativeTo == Corner.Top)
                CorrectedPosition = new Vector2(UserInterface.Width / 2 - Size.X / 2 + Position.X, -Position.Y - Size.Y);
            else if (RelativeTo == Corner.Center)
                CorrectedPosition = new Vector2(UserInterface.Width / 2 - Size.X / 2 + Position.X, UserInterface.Height / 2 - Size.Y / 2 + Position.Y);
        }
        else
        {
            if (RelativeTo == Corner.BottomLeft) CorrectedPosition = Position;
            else if (RelativeTo == Corner.TopLeft)
                CorrectedPosition = new Vector2(Position.X, Parent.Size.Y - Position.Y - Size.Y);
            else if (RelativeTo == Corner.BottomRight)
                CorrectedPosition = new Vector2(Parent.Size.X - Position.X - Size.X, Position.Y);
            else if (RelativeTo == Corner.TopRight)
                CorrectedPosition = new Vector2(Parent.Size.X - Position.X - Size.X, Parent.Size.Y - Position.Y - Size.Y);
            else if (RelativeTo == Corner.Bottom)
                CorrectedPosition = new Vector2(Parent.Size.X / 2 - Size.X / 2 + Position.X, Position.Y);
            else if (RelativeTo == Corner.Top)
                CorrectedPosition = new Vector2(Parent.Size.X / 2 - Size.X / 2 + Position.X, Parent.Size.Y - Position.Y - Size.Y);
            else if (RelativeTo == Corner.Fill)
            {
                CorrectedPosition = new Vector2(0, 0);
                Size = Parent.Size;
            }
            else if (RelativeTo == Corner.Center)
                CorrectedPosition = new Vector2(Parent.Size.X / 2 - Size.X / 2 + Position.X, Parent.Size.Y / 2 - Size.Y / 2 + Position.Y);
            CorrectedPosition += Parent.CorrectedPosition;
        }
    }

}


public struct CornerPosition(Vector2 position, Vector2 size)
{
    public Vector2 TopLeft => position;
    public Vector2 TopRight => new(position.X + size.X, position.Y);
    public Vector2 BottomLeft => new(position.X, position.Y + size.Y);
    public Vector2 BottomRight => new(position.X + size.X, position.Y + size.Y);
    public Vector2 Center => new(position.X - size.X / 2, position.Y - size.Y / 2);

    public Vector2 CenterTop => new (Center.X, position.Y);
    public Vector2 CenterRight => new (TopRight.X, Center.Y);
    public Vector2 CenterBottom => new (Center.X, BottomRight.Y);
    public Vector2 CenterLeft => new (TopLeft.X, Center.Y);
}