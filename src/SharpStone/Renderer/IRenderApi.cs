using SharpStone.Core;
using SharpStone.Maths;

namespace SharpStone.Renderer;
public interface IRenderApi : IService
{
    IRenderCommands Commands {  get; }
    IRenderFactory Factory { get; }
}

public interface IRenderCommands
{
    void SetViewPort(int x, int y, int width, int height);
    void SetClearColor(Color color);
    void Clear();
    void DrawIndexed(IVertexArray vertexArray, int? indexCount = null);
    void DrawLines(IVertexArray vertexArray, int indexCount);
    void DrawArrays(IVertexArray vertexArray, int vertexCount);
    void SetLineWidth(int width);
}

public interface IRenderFactory
{
    IGraphicsContext CreateGrapicsContext(IWindow window);
    IVertexArray CreateVertexArray();
    IVertextBuffer CreateVertexBuffer<T>(T[] data, int size) where T : struct;
    IIndexBuffer CreateIndexBuffer(uint[] indices, int size);
    IUniformBuffer CreateUniformBuffer();

    ITexture2D CreateTexture(uint width, uint heigth);
    ITexture2D CreateTexture(string path);

    IShader CreateShader(string name);
    IShader CreateShader(string name, string vertexSrc, string fragmentSrc);
}