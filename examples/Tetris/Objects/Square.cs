using SharpStone.Maths;

namespace Tetris.Objects;
public class Square
{
    public bool IsFilled { get; set; }
    public bool IsActive { get; set; }
    public bool ToBeDeleted { get; set; }
    public Color Color { get; set; }
}
