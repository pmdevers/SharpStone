using System.Numerics;

namespace SharpStone.Graphics;
public class Camera
{
    public Matrix4x4 GetViewProjection()
    {
        return Matrix4x4.Identity;
    }
}
