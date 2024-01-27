using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Graphics;
public unsafe class IndexBuffer
{
    private readonly uint[] _indeces;

    private uint _id;
    private readonly int _size;
    public int Count => _indeces.Length;

    public static IndexBuffer Create(uint[] indeces)
        => new(indeces);

    private IndexBuffer(uint[] indeces)
    {
        _indeces = indeces;
        _id = glGenBuffer();
        glBindBuffer(BufferTargetARB.ElementArrayBuffer, _id);
        glBufferData(BufferTargetARB.ElementArrayBuffer, indeces, BufferUsageARB.StaticDraw);
    }

    public void Bind()
    {
        glBindBuffer(BufferTargetARB.ElementArrayBuffer, _id);
    }

    public void Dispose()
    {
        if (_id != 0)
        {
            fixed (uint* pId = &_id)
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

