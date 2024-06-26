﻿using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;
using System.Runtime.InteropServices;
using SharpStone.Core;

namespace SharpStone.Graphics;

public unsafe class VertexBuffer
{
    private readonly uint _vbo;
    public BufferLayout Layout { get; set; } = new BufferLayout();

    public static VertexBuffer Create<T>(T[] data) where T : struct
    {
        var size = Marshal.SizeOf(typeof(T)) * data.Length;
        var vba = Create(size);
        vba.SetData(data);
        return vba;
    }
       
    public static VertexBuffer Create(int size)
        => new(size);


    private VertexBuffer(int size)
    {
        _vbo = glGenBuffer();
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        glBufferData(BufferTargetARB.ArrayBuffer, size, null, BufferUsageARB.DynamicDraw);
    }

    //private VertexBuffer(object[] data, int itemSize)
    //{
    //    var size = itemSize * data.Length; //Marshal.SizeOf(typeof(T)) * data.Length;
    //    _vbo = glGenBuffer();

    //    glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
    //    fixed (void* pData = data)
    //    {
    //        glBufferData(BufferTargetARB.ArrayBuffer, size, pData, BufferUsageARB.StaticDraw);
    //    }
    //}

    public void Bind()
    {
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
    }

    public void Dispose()
    {
        uint[] _ids = [_vbo];
        fixed (uint* ptr = _ids)
        {
            glDeleteBuffers(1, ptr);
        }
    }

    public void SetData<T>(T[] data)
    {
        var size = Marshal.SizeOf(typeof(T)) * data.Length;  
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        fixed (void* pData = data)
        {
            glBufferSubData(BufferTargetARB.ArrayBuffer, 0, size, pData);
        }
    }

    public void SetData(object[] data, int itemSize)
    {
        var size = itemSize * data.Length;
        glBindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        fixed (void* pData = data)
        {
            glBufferSubData(BufferTargetARB.ArrayBuffer, 0, size, pData);
        }
    }

    public void Unbind()
    {
        glBindBuffer(BufferTargetARB.ArrayBuffer, 0);
    }
}


public enum ShaderDataType
{
    None = 0, Float, Float2, Float3, Float4, Mat3, Mat4, Int, Int2, Int3, Int4, Bool
}

public class BufferLayout
{
    private int _stride = 0;
    private readonly List<BufferElement> _elements = [];

    public BufferLayout()
    {
        CalculateOffsetAndStride();
    }

    public int Stride => _stride;
    public BufferElement[] Elements => _elements.ToArray();
    public void CalculateOffsetAndStride()
    {
        int offset = 0;
        _stride = 0;
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i] = _elements[i] with
            {
                Offset = offset
            };
            offset += _elements[i].Size;
            _stride += _elements[i].Size;
        }
    }
    public void Add(string name, ShaderDataType type)
        => _elements.Add(new(type, name));
}

public struct BufferElement(ShaderDataType type, string name, bool normalized = false)
{
    public string Name = name;
    public ShaderDataType Type = type;
    public int Offset { get; set; }
    public readonly int Size => ShaderDataTypeSize(Type);
    public bool Normalized = normalized;

    public readonly int GetComponentCount()
    {
        switch (Type)
        {
            case ShaderDataType.Float: return 1;
            case ShaderDataType.Float2: return 2;
            case ShaderDataType.Float3: return 3;
            case ShaderDataType.Float4: return 4;
            case ShaderDataType.Mat3: return 3; // 3* float3
            case ShaderDataType.Mat4: return 4; // 4* float4
            case ShaderDataType.Int: return 1;
            case ShaderDataType.Int2: return 2;
            case ShaderDataType.Int3: return 3;
            case ShaderDataType.Int4: return 4;
            case ShaderDataType.Bool: return 1;
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
