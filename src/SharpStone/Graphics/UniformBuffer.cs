using SharpStone.Platform.OpenGL;
using System.Runtime.InteropServices;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Graphics;
public unsafe class UniformBuffer : IDisposable
{
    private uint _id;

    public static UniformBuffer Create(int size, uint binding)
        => new(size, binding);

    public static UniformBuffer Create<T>(uint binding)
        => new(Marshal.SizeOf<T>(), binding);

    private UniformBuffer(int size, uint binding)
    {
        _id = glCreateBuffer();
        glNamedBufferData(_id, size, null, VertexBufferObjectUsage.DynamicDraw);
        glBindBufferBase(BufferTargetARB.UniformBuffer, binding, _id);
    }

    public void SetData<T>(T data, nint offset = 0)
    {
        var sizeOf = Marshal.SizeOf<T>();
        glNamedBufferSubData(_id, offset, sizeOf, &data);
    }

    ~UniformBuffer()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (_id > 0)
        {
            fixed (uint* ptr = &_id)
            {
                glDeleteBuffers(1, ptr);
                _id = 0;
            }
        }
        GC.SuppressFinalize(this);
    }
}
