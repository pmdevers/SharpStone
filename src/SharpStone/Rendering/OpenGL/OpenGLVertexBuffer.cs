using SharpStone.Platform.OpenGL;
using System.Runtime.InteropServices;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Rendering.OpenGL;

internal unsafe class OpenGLVertexBuffer<T> : IVertexBuffer<T>
    where T : struct
{
    private readonly uint _vbo;
    private int _size;
    private T[]? _data;

    public OpenGLVertexBuffer(int size)
    {
        _data = null;
        _size = size;
        _vbo = glGenBuffer();
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        glBufferData(BufferTargetARB.ArrayBuffer, size, null, BufferUsageARB.DynamicDraw);
    }

    public OpenGLVertexBuffer(T[] data)
    {
        _data = data;
        _size = Marshal.SizeOf(typeof(T)) * data.Length; 
        _vbo = glGenBuffer();

        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        glBufferData(BufferTargetARB.ArrayBuffer, _data, BufferUsageARB.StaticDraw);
    }

    public BufferLayout Layout { get; set; } = [];

    public void Bind()
    {
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
    }

    public void Dispose()
    {
        uint[] _ids = [_vbo];
        fixed (uint* ptr = _ids) { 
            glDeleteBuffers(1, ptr);
        }
    }

    public void SetData(T[] data)
    {
        _data = data;
        _size = Marshal.SizeOf(typeof(T)) * data.Length;
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        glBufferSubData(BufferTargetARB.ArrayBuffer, data);
    }

    public void Unbind()
    {
        glBindBuffer(BufferTargetARB.ArrayBuffer, 0);
    }
}