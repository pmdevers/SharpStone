using SharpStone.Maths;
using SharpStone.Platform.Win32.DXGI;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static SharpStone.Application;

namespace SharpStone.Rendering.OpenGL;

public struct QuadVertex
{
    public Vector3 Position;
    public Vector4 Color;
    public Vector2 TexCoord;
    public float TexIndex;
    public float TilingFactor;

    // Editor-only
    public int EntityID;
};

internal unsafe struct Renderer2DData()
{
    public const int MaxQuads = 20000;
    public const int MaxVertices = MaxQuads * 4;
    public const int MaxIndices = MaxQuads * 6;
    public const int MaxTextureSlots = 32;
    
    public IVertexArray QuadVertexArray;
    public IVertexBuffer<QuadVertex> QuadVertexBuffer;
    public IShader QuadShader;
    public ITexture2D WhiteTexture;

    public int QuadIndexCount = 0;
    public int QuadVertexCount = 0;
    public QuadVertex[] QuadVertexBufferData;
    public static QuadVertex[] QuadVertexBufferBase = new QuadVertex[MaxVertices];
    public Vector4[] QuadVertexPositions = new Vector4[4];
}

internal unsafe class OpenGLRenderer2D : IRenderer2D
{
    private static Renderer2DData _data = new();

    public IVertexArray VertexArray => _data.QuadVertexArray;

    //private IVertexArray CircleVertexArray;
    //private IVertexBuffer CircleVertexBuffer;
    //private IShader CircleShader;
    //private IVertexArray LineVertexArray;
    //private IVertexBuffer _vertexBuffer;
    //private IShader LineShader;
    //private IVertexArray TextVertexArray;
    //private IVertexBuffer TextVertexBuffer;
    //private IShader TextShader;

    public bool Init()
    {
        _data.QuadVertexArray = Renderer.Factory.CreateVertexArray();
        _data.QuadVertexBuffer = Renderer.Factory.CreateVertexBuffer<QuadVertex>(Renderer2DData.MaxVertices * sizeof(QuadVertex));

        _data.QuadVertexBuffer.Layout.Add("a_Position", ShaderDataType.Float3);
        _data.QuadVertexBuffer.Layout.Add("a_Color", ShaderDataType.Float4);
        _data.QuadVertexBuffer.Layout.Add("a_TextCoord", ShaderDataType.Float2);
        _data.QuadVertexBuffer.Layout.Add("a_TexIndex", ShaderDataType.Float);
        _data.QuadVertexBuffer.Layout.Add("a_TilingFactor", ShaderDataType.Float);
        _data.QuadVertexBuffer.Layout.Add("a_EntityID", ShaderDataType.Int);

        _data.QuadVertexArray.AddVertexBuffer(_data.QuadVertexBuffer);

        uint[] quadIndices = new uint[Renderer2DData.MaxIndices];
        uint offset = 0;
        for (uint i = 0; i < Renderer2DData.MaxIndices; i += 6)
        {
            quadIndices[i + 0] = offset + 0;
            quadIndices[i + 1] = offset + 1;
            quadIndices[i + 2] = offset + 2;
            quadIndices[i + 3] = offset + 3;
            quadIndices[i + 4] = offset + 4;
            quadIndices[i + 5] = offset + 5;

            offset += 4;
        }

        var quadIB = Renderer.Factory.CreateIndexBuffer(quadIndices);
        _data.QuadVertexArray.SetIndexBuffer(quadIB);

        _data.QuadVertexPositions[0] = new Vector4(-0.5f, -0.5f, 0.0f, 1.0f);
        _data.QuadVertexPositions[1] = new Vector4( 0.5f, -0.5f, 0.0f, 1.0f);
        _data.QuadVertexPositions[2] = new Vector4( 0.5f,  0.5f, 0.0f, 1.0f);
        _data.QuadVertexPositions[3] = new Vector4(-0.5f,  0.5f, 0.0f, 1.0f);

        return true;
    }

    public void BeginScene(ICamera camera, Matrix4 transform)
    {
        // TODO Update Camera ViewProjection

        StartBatch();
    }

    public void DrawQuad(Vector2 position, Vector2 size, Color color)
    {
        DrawQuad(new Vector3(position, 0f), size, color);
    }

    public void DrawQuad(Vector3 position, Vector2 size, Color color)
    {
        var transform = Matrix4.CreateTranslation(position)
            * Matrix4.CreateScaling(new Vector3(size, 1.0f));

        DrawQuad(transform, color);
    }

    public void DrawQuad(Vector2 position, Vector2 size, ITexture2D texture, float tilingFactor = 1)
    {
        DrawQuad(position, size, texture, Color.White, tilingFactor);
    }

    public void DrawQuad(Vector2 position, Vector2 size, ITexture2D texture, Color color, float tilingFactor = 1)
    {
        DrawQuad(new Vector3(position, 0f), size, texture, Color.White, tilingFactor);
    }

    private void DrawQuad(Vector3 position, Vector2 size, ITexture2D texture, Color color, float tilingFactor)
    {
        var transform = Matrix4.CreateTranslation(position)
            * Matrix4.CreateScaling(new Vector3(size, 1.0f));

        DrawQuad(transform, texture, color, tilingFactor);
    }

    private void DrawQuad(Matrix4 transform, ITexture2D texture, Color color, float tilingFactor)
    {
        
    }

    public void DrawQuad(Matrix4 transform, Color color, int entityId = -1)
    {
        var quadVertexCount = 4;
        float textureIndex = 0.0f;
        Vector2[] textureCoords = [
            new(0, 0),
            new(1, 0),
            new(1, 1),
            new(0, 1),
        ];
        float tillingFactor = 1.0f;

        //if(_data.QuadIndexCount >= Renderer2DData.MaxIndices)
        //    NextBatch();

        for (int i = 0; i < quadVertexCount; i++)
        {
            var item = _data.QuadVertexBufferData[_data.QuadVertexCount];
            var t = _data.QuadVertexPositions[i] * transform;

            item.Position = new Vector3(t.X, t.Y, t.Z);
            item.Color = new Vector4(color.R, color.G, color.B, color.A);
            item.TexCoord = textureCoords[i];
            item.TexIndex = textureIndex;
            item.TilingFactor = tillingFactor;
            item.EntityID = entityId;
            
            _data.QuadVertexCount++;
            
        }
        _data.QuadIndexCount += 6;
    }

    public void EndScene()
    {
        Flush();
    }

    public void Flush()
    {
        if(_data.QuadIndexCount > 0)
        {
            var data = _data.QuadVertexBufferData.Take(_data.QuadVertexCount).ToArray();

            _data.QuadVertexBuffer.SetData(data);
        }
    }

    public bool Shutdown()
    {
        throw new NotImplementedException();
    }

    private void StartBatch()
    {
        _data.QuadIndexCount = 0;
        _data.QuadVertexBufferData = Renderer2DData.QuadVertexBufferBase;


    }
}
