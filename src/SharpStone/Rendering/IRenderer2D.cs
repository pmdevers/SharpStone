using SharpStone.Maths;
using System.Numerics;

namespace SharpStone.Rendering;
public interface IRenderer2D
{
    bool Init();
    bool Shutdown();

    void BeginScene(ICamera camera, Matrix4x4 transform);
    void EndScene();
    void Flush();

    void DrawQuad(Vector2 position, Vector2 size, Color color);
}

public interface ICamera
{
    void Update(float deltaTime);
}
