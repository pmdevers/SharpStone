using System.Numerics;

namespace SharpStone.Graphics;
public class Camera
{
    public virtual Matrix4x4 ProjectionView { get; set;} = Matrix4x4.Identity;
}
