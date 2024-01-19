using SharpStone.Maths;

namespace SharpStone.Rendering;
public interface IRenderer2D
{
    IVertexArray VertexArray { get; }


    bool Init();
    bool Shutdown();

    void BeginScene(ICamera camera, Matrix4 transform);
    void EndScene();
    void Flush();


    // Pimitives
    void DrawQuad(Vector2 position, Vector2 size, Color color);
    void DrawQuad(Vector3 position, Vector2 size, Color color);
    void DrawQuad(Vector2 position, Vector2 size, ITexture2D texture, float tilingFactor = 1.0f);
    void DrawQuad(Vector2 position, Vector2 size, ITexture2D texture, Color color, float tilingFactor = 1.0f);

    void DrawQuad(Matrix4 transform, Color color, int entityId = -1);

}

public interface ICamera
{
    void Update(float deltaTime);
}
