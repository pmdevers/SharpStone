using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;
using SharpStone.Core;

namespace SharpStone.Graphics;

public unsafe class VertexArray
{
    private readonly uint _vba;

    private IndexBuffer? _indexBuffer;
    private readonly List<VertexBuffer> _buffers = [];

    public static VertexArray Create() 
        => new();

    private VertexArray()
    {
        _vba = glGenVertexArray();
    }

    public IndexBuffer? GetIndexBuffer() => _indexBuffer;

    public void SetIndexBuffer(IndexBuffer indexBuffer)
    {
        glBindVertexArray(_vba);
        indexBuffer.Bind();
        _indexBuffer = indexBuffer;
    }

    public void AddVertexBuffer(VertexBuffer vertextBuffer)
    {
        glBindVertexArray(_vba);
        vertextBuffer.Bind();

        vertextBuffer.Layout.CalculateOffsetAndStride();

        var stride = vertextBuffer.Layout.Stride;

        uint index = 0;
        foreach (var element in vertextBuffer.Layout.Elements)
        {
            switch (element.Type)
            {
                case ShaderDataType.Float:
                case ShaderDataType.Float2:
                case ShaderDataType.Float3:
                case ShaderDataType.Float4:
                    {
                        glEnableVertexAttribArray(index); //((uint)_buffers.Count); 
                        glVertexAttribPointer(
                            index, // (uint)_buffers.Count, 
                            element.GetComponentCount(),
                            ShaderDataTypeToOpenGLBaseType(element.Type),
                            element.Normalized,
                            stride,
                            element.Offset);
                        break;
                    }
                case ShaderDataType.Int:
                case ShaderDataType.Int2:
                case ShaderDataType.Int3:
                case ShaderDataType.Int4:
                case ShaderDataType.Bool:
                    {
                        glEnableVertexAttribArray(index); //((uint)_buffers.Count);
                        glVertexAttribIPointer(
                            index, //(uint)_buffers.Count,
                            element.GetComponentCount(),
                            ShaderDataTypeToOpenGLBaseType(element.Type),
                            stride,
                            element.Offset);
                        break;
                    }
                case ShaderDataType.Mat3:
                case ShaderDataType.Mat4:
                    {
                        var count = element.GetComponentCount();
                        var pCount = element.Offset + sizeof(float) * count * 1;
                        for (int i = 0; i < count; i++)
                        {
                            glEnableVertexAttribArray((uint)_buffers.Count);
                            glVertexAttribPointer(
                                index, //(uint)_buffers.Count,
                                element.GetComponentCount(),
                                ShaderDataTypeToOpenGLBaseType(element.Type),
                                element.Normalized,
                                stride,
                                pCount);

                            glVertexAttribDivisor((uint)_buffers.Count, 1);
                        }
                        break;
                    }
                default:
                    Logger.Assert<VertexArray>(false, "Unknown ShaderDataType!");
                    break;
            }
            index++;
        }


        //glEnableVertexAttribArray(0);
        //glVertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

        _buffers.Add(vertextBuffer);
    }

    public void Bind()
    {
        glBindVertexArray(_vba);
    }

    public void Dispose()
    {

    }

    public VertexBuffer[] GetVertextBuffers()
        => _buffers.ToArray();
    public void Unbind()
    {
    }

    private static VertexAttribPointerType ShaderDataTypeToOpenGLBaseType(ShaderDataType type)
    {
        switch (type)
        {
            case ShaderDataType.Float:
            case ShaderDataType.Float2:
            case ShaderDataType.Float3:
            case ShaderDataType.Float4:
            case ShaderDataType.Mat3:
            case ShaderDataType.Mat4:
                return VertexAttribPointerType.Float;
            case ShaderDataType.Int:
            case ShaderDataType.Int2:
            case ShaderDataType.Int3:
            case ShaderDataType.Int4:
                return VertexAttribPointerType.Int;
            case ShaderDataType.Bool:
                return VertexAttribPointerType.Byte;
            default:
                Logger.Error<VertexArray>("Unknown ShaderDataType!");
                return 0;
        }
    }
}
