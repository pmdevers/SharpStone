using SharpStone.Core;

using static SharpStone.Logging;

namespace SharpStone.Renderer;

public interface IVertextBuffer : IRenderObject
{
    BufferLayout Layout { get; }
}

public enum ShaderDataType
{
    None = 0, Float, Float2, Float3, Float4, Mat3, Mat4, Int, Int2, Int3, Int4, Bool
}

public class BufferLayout : List<BufferElement>
{
    private int _stride = 0;

    public BufferLayout()
    {
        CalculateOffsetAndStride();
    }

    public int Stride => _stride;
    public BufferElement[] Elements => ToArray();
    private void CalculateOffsetAndStride()
    {
        int offset = 0;
        _stride = 0;
        for (int i = 0; i < Count; i++)
        {
            BufferElement element = this[i];
            element.Offset = offset;
            offset += element.Size;
            _stride += element.Size;
        }
    }
}

public struct BufferElement
{
    public string Name;
    public ShaderDataType Type;
    public int Offset;
    public int Size;
    public bool Normalized;

    public BufferElement(){ }
    public BufferElement(ShaderDataType type, string name, bool normalized)
    {
        Name = name;
        Type = type;
        Size = ShaderDataTypeSize(type);
        Offset = 0;
        Normalized = normalized;
    }

    public int GetComponentCount() 
		{
			switch (Type)
			{
				case ShaderDataType.Float:   return 1;
				case ShaderDataType.Float2:  return 2;
				case ShaderDataType.Float3:  return 3;
				case ShaderDataType.Float4:  return 4;
				case ShaderDataType.Mat3:    return 3; // 3* float3
				case ShaderDataType.Mat4:    return 4; // 4* float4
				case ShaderDataType.Int:     return 1;
				case ShaderDataType.Int2:    return 2;
				case ShaderDataType.Int3:    return 3;
				case ShaderDataType.Int4:    return 4;
				case ShaderDataType.Bool:    return 1;
                default: 
                    Logger.Assert<BufferElement>(false, "Unknown ShaderDataType!");
                    return 0;
			}
		}

    private static int ShaderDataTypeSize(ShaderDataType type)
    {
        switch (type)
        {
            case ShaderDataType.Float: return 4;
            case ShaderDataType.Float2: return 4 * 2;
            case ShaderDataType.Float3: return 4 * 3;
            case ShaderDataType.Float4: return 4 * 4;
            case ShaderDataType.Mat3: return 4 * 3 * 3;
            case ShaderDataType.Mat4: return 4 * 4 * 4;
            case ShaderDataType.Int: return 4;
            case ShaderDataType.Int2: return 4 * 2;
            case ShaderDataType.Int3: return 4 * 3;
            case ShaderDataType.Int4: return 4 * 4;
            case ShaderDataType.Bool: return 1;
            default:
                Logger.Assert<BufferElement>(false, "Unknown ShaderDataType!");
                return 0;
        }
    }
}
