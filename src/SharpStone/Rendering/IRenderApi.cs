using SharpStone.Core;
using SharpStone.Maths;

namespace SharpStone.Rendering;
public interface IRenderApi
{
    IRenderCommands Commands {  get; }
    IRenderFactory Factory { get; }

    IRenderer2D Renderer2D { get; }
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
    IVertexBuffer<T> CreateVertexBuffer<T>(T[] data) where T : struct;
    IVertexBuffer<T> CreateVertexBuffer<T>(int size) where T : struct;
    IIndexBuffer CreateIndexBuffer(uint[] indices);
    IUniformBuffer CreateUniformBuffer();

    ITexture2D CreateTexture(uint width, uint heigth);
    ITexture2D CreateTexture(string path);

    IShader CreateShader(string name);
    IShader CreateShader(string name, string vertexSrc, string fragmentSrc);
}