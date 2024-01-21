using System.Numerics;

namespace SharpStone.Graphics;
public class Camera
{
    public virtual Matrix4x4 ProjectionView { get; protected set;}
        = Matrix4x4.CreateTranslation(0f, 0f, 0f);
}
