using SharpStone.Maths;
using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Rendering.OpenGL;

public unsafe class OpenGLCommands : IRenderCommands
{
    public static readonly OpenGLCommands Instance = new();

    public void Clear()
    {
        glClear((uint)(AttribMask.ColorBufferBit | AttribMask.DepthBufferBit));
    }

    public void DrawArrays(IVertexArray vertexArray, int vertexCount)
    {
        vertexArray.Bind();
        glDrawArrays(PrimitiveType.Triangles, 0, vertexCount);
    }

    public void DrawIndexed(IVertexArray vertexArray, int? indexCount = null)
    {
        vertexArray.Bind();
        int count = indexCount ?? vertexArray.GetIndexBuffer().Count;
        glDrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, null);
    }

    public void DrawLines(IVertexArray vertexArray, int indexCount)
    {
        vertexArray.Bind();
        glDrawArrays(PrimitiveType.Lines, 0, indexCount);
    }

    public void SetClearColor(Color color)
    {
        glClearColor(color.R, color.G, color.B, color.A);
    }

    public void SetLineWidth(int width)
    {
        glLineWidth(width);
    }

    public void SetViewPort(int x, int y, int width, int height)
    {
        glViewport(x, y, width, height);
    }
}
