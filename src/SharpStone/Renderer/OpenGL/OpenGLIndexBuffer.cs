using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;
using SharpStone.Platform.OpenGL;

namespace SharpStone.Renderer.OpenGL;
internal unsafe class OpenGLIndexBuffer : IIndexBuffer
{
    private readonly uint[] _indeces;

    private uint _id;
    public int Count => _indeces.Length;

    public OpenGLIndexBuffer(uint[] indeces, int size)
    {
        _indeces = indeces;
        _id = glGenBuffer();
        glBindBuffer(BufferTargetARB.ElementArrayBuffer, _id);
        glBufferData(BufferTargetARB.ElementArrayBuffer, size, indeces, BufferUsageARB.StaticDraw);
    }

    public void Bind()
    {
        glBindBuffer(BufferTargetARB.ElementArrayBuffer, _id);
    }

    public void Dispose()
    {
        if( _id != 0 )
        {
            fixed(uint* pId = &_id)
            {
                glDeleteBuffers(1, pId);
                _id = 0;
            }
        }
        GC.SuppressFinalize(this);
    }

    public void Unbind()
    {
        glBindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }
}
