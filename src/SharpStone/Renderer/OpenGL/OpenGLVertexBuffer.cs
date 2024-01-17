using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Renderer.OpenGL;

internal unsafe class OpenGLVertexBuffer<T> : IVertextBuffer
    where T : struct
{
    private uint _vbo;
    private T[] _data;
    public OpenGLVertexBuffer(T[] data, int size)
    {
        _data = data;
        _vbo = glGenBuffer();
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        glBufferData(BufferTargetARB.ArrayBuffer, size, _data, BufferUsageARB.StaticDraw);
    }

    public BufferLayout Layout { get; set; } = new BufferLayout([]);

    public void Bind()
    {
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
    }

    public void Dispose()
    {
        //glDeleteBuffers(1, _vbo);
    }

    public void Unbind()
    {
        
    }
}