using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;
using SharpStone.Core;

namespace SharpStone.Renderer.OpenGL;
internal unsafe class OpenGLVertexArray : IVertexArray
{
    private uint _vba;

    private IIndexBuffer? _indexBuffer;
    private List<IVertextBuffer> _buffers = [];

    public OpenGLVertexArray()
    {
        _vba = glGenVertexArray();
    }

    public IIndexBuffer? GetIndexBuffer() => _indexBuffer;

    public void SetIndexBuffer(IIndexBuffer indexBuffer)
    { 
        glBindVertexArray(_vba);
        indexBuffer.Bind();
        _indexBuffer = indexBuffer;
    }

    public void AddVertexBuffer(IVertextBuffer vertextBuffer)
    {
        glBindVertexArray(_vba);
        vertextBuffer.Bind();

        var stride  = vertextBuffer.Layout.Stride;

        foreach (var element in vertextBuffer.Layout)
        {
            switch(element.Type)
            {
                case ShaderDataType.Float:
                case ShaderDataType.Float2:
                case ShaderDataType.Float3:
                case ShaderDataType.Float4:
                    {
                        glEnableVertexAttribArray((uint)_buffers.Count + 1); 
                        glVertexAttribPointer(
                            (uint)_buffers.Count + 1, 
                            element.GetComponentCount(), 
                            ShaderDataTypeToOpenGLBaseType(element.Type),
                            element.Normalized,
                            stride, 
                            &element.Offset);
                        break;
                    }
                case ShaderDataType.Int:
                case ShaderDataType.Int2:
                case ShaderDataType.Int3:
                case ShaderDataType.Int4:
                case ShaderDataType.Bool:
                    {
                        glEnableVertexAttribArray((uint)_buffers.Count + 1);
                        glVertexAttribIPointer(
                            (uint)_buffers.Count + 1,
                            element.GetComponentCount(),
                            ShaderDataTypeToOpenGLBaseType(element.Type),
                            stride,
                            &element.Offset);
                        break;
                    }
                case ShaderDataType.Mat3:
                case ShaderDataType.Mat4:
                    {
                        var count = element.GetComponentCount();
                        var pCount = element.Offset + sizeof(float) * count * 1;
                        for (int i = 0; i < count; i++)
                        {
                            glEnableVertexAttribArray((uint)_buffers.Count + 1);
                            glVertexAttribPointer(
                                (uint)_buffers.Count + 1,
                                element.GetComponentCount(),
                                ShaderDataTypeToOpenGLBaseType(element.Type),
                                element.Normalized,
                                stride,
                                &pCount);

                            glVertexAttribDivisor((uint)_buffers.Count + 1, 1);
                        }
                        break;
                    }
                default:
                    Logger.Assert<OpenGLVertexArray>(false, "Unknown ShaderDataType!");
                    break;
            }    
        }


        glEnableVertexAttribArray(0);
        glVertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, null);

        _buffers.Add(vertextBuffer);
    }

    public void Bind()
    {
        glBindVertexArray(_vba);
    }

    public void Dispose()
    {
        
    }

    public IVertextBuffer[] GetVertextBuffers()
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
                Logger.Error<OpenGLVertexArray>("Unknown ShaderDataType!");
                return 0;
        }
    }
}
