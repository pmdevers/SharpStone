using SharpStone.Maths;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SharpStone.Graphics;

public struct CameraData
{
    public Matrix4x4 ProjectionView;
};

public class Renderer
{
    #region Quads
    public const int MaxQuads = 20000;
    public const int VerticesPerQuad = 4;
    public const int IndicesPerQuad = 6;

    public const int MaxVertices = MaxQuads * VerticesPerQuad;
    public const int MaxIndices = MaxQuads * IndicesPerQuad;

    private static VertexArray _quadVertexArray;
    private static Shader _quadShader;

    private static readonly List<QuadVertex> _quads = new(MaxVertices);
    private static readonly uint[] _quadIndices = new uint[MaxIndices];
    private static int _quadIndexCount = 0;

    private static readonly Vector4[] _baseQuadPositions = [
        new Vector4( -0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(  0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(  0.5f,  0.5f, 0.0f, 1.0f),
        new Vector4( -0.5f,  0.5f, 0.0f, 1.0f)
    ];

    #endregion

    #region Textures

    public const int MaxTextureSlots = 32;

    private static Texture2D _whiteTexture;

    private static readonly List<Texture2D> TextureSlots = [];

    private static Vector2[] defaultTextureCoords = [
        new(0.0f, 0.0f ),
        new(1.0f, 0.0f ),
        new(1.0f, 1.0f ),
        new(0.0f, 1.0f )
    ];


    #endregion

    private static UniformBuffer _cameraUnformBuffer;
    private static CameraData _cameraBuffer;

    public static bool Init()
    {
        #region Quads
        _quadVertexArray = VertexArray.Create();

        uint offset = 0;
        for (uint i = 0; i < MaxIndices; i += IndicesPerQuad)
        {
            _quadIndices[i + 0] = 0 + offset;
            _quadIndices[i + 1] = 1 + offset;
            _quadIndices[i + 2] = 2 + offset;
            _quadIndices[i + 3] = 2 + offset;
            _quadIndices[i + 4] = 3 + offset;
            _quadIndices[i + 5] = 0 + offset;

            offset += VerticesPerQuad;
        }

        var vbo1 = VertexBuffer.Create(MaxVertices);
        var ib = IndexBuffer.Create(_quadIndices);

        vbo1.Layout.Add("positions", ShaderDataType.Float3);
        vbo1.Layout.Add("colors", ShaderDataType.Float4);
        vbo1.Layout.Add("texCoord", ShaderDataType.Float2);
        vbo1.Layout.Add("texIndex", ShaderDataType.Float);
        vbo1.Layout.Add("tiling", ShaderDataType.Float);

        _quadVertexArray.AddVertexBuffer(vbo1);
        _quadVertexArray.SetIndexBuffer(ib);

        _quadShader = Shader.Create("UISolid");
        _quadShader.Bind();
        #endregion

        #region Textures

        _whiteTexture = Texture2D.Create(new TextureSpecification());
        _whiteTexture.SetData([ 0xffffffff ]);

        TextureSlots.Add( _whiteTexture );

        #endregion
        _cameraUnformBuffer = UniformBuffer.Create<CameraData>(0);



        return true;
    }

    public static void BeginScene(Camera camera)
    {
        _cameraBuffer.ProjectionView = camera.ProjectionView;
        _cameraUnformBuffer.SetData(_cameraBuffer);
        StartBatch();
    }
    public static void BeginScene(Camera camera, Matrix4x4 transform)
    {
        Matrix4x4.Invert(transform, out var inverse);
        _cameraBuffer.ProjectionView = camera.ProjectionView * inverse;
        _cameraUnformBuffer.SetData(_cameraBuffer);

        StartBatch();
    }
    public static void EndScene()
    {
        Flush();
    }
    public static void Flush()
    {
        if(_quads.Count > 0)
        {
            _quadVertexArray.GetVertextBuffers()[0]
                .SetData(_quads.ToArray());

            for(int i = 0; i < TextureSlots.Count; i++)
            {
                TextureSlots[i].Bind((uint)i);
            }

            _quadShader.Bind();
            
            RenderCommand.DrawIndexed(_quadVertexArray, _quads.Count / 4 * 6);
        }
        
    }

    public static void StartBatch()
    {
        _quadIndexCount = 0;
        _quads.Clear();
    }

    public static void NextBatch()
    {
        Flush();
        StartBatch();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuadVertex(Vector3 position, Vector4 color, Vector2 textCoord, float texIndex, float tilingFactor)
    {
        public Vector3 Position = position;
        public Vector4 Color = color;
        public Vector2 TexCoord = textCoord;
        public float TexIndex = texIndex;
        public float TilingFactor = tilingFactor;
    }


    public static void DrawQuad(Vector2 position, Vector2 size, Texture2D texture, float tilingFactor, Color color)
    {
        var v3Position = new Vector3(position, 1.0f);
        var v3Size = new Vector3(size, 1.0f);

        var translation = Matrix4x4.CreateTranslation(v3Position);
        var scaling = Matrix4x4.CreateScale(v3Size);
        var transform = translation + scaling;

        DrawQuad(transform, texture, tilingFactor, color);
    }

    public static void DrawQuad(Matrix4x4 transform, Texture2D texture, float tilingFactor, Color color)
    {
        Vector2[] textureCoords = [ new(0, 0), new(1, 0), new(1, 1), new (0, 1) ];

        if (!TextureSlots.Contains(texture))
        {
            TextureSlots.Add(texture);
        }

        if (_quads.Count >= MaxVertices)
            NextBatch();

        for (int i = 0; i < _baseQuadPositions.Length; i++)
        {
            var t = Vector4.Transform(_baseQuadPositions[i], transform);
            
            var quadVertex = new QuadVertex
            {
                Position = new Vector3(t.X, t.Y, t.Z),
                Color = new Vector4(color.R, color.G, color.B, color.A),
                TexCoord = textureCoords[i],
                TexIndex = TextureSlots.IndexOf(texture),
                TilingFactor = tilingFactor,
                
            };
            _quads.Add(quadVertex);
        }
    }
    public static void DrawQuad(Vector2 position, Vector2 size, Color color)
    {
        var v3Position = new Vector3(position, 1.0f);
        var v3Size = new Vector3(size, 1.0f);

        var translation = Matrix4x4.CreateTranslation(v3Position);
        var scaling = Matrix4x4.CreateScale(v3Size);
        var transform = translation + scaling;

        DrawQuad(transform, color);
    }
    public static void DrawQuad(Matrix4x4 transform, Color color, int entityId = 0)
    {
        if (_quads.Count >= MaxVertices)
            NextBatch();

        for (int i = 0; i < _baseQuadPositions.Length; i++)
        {
            var t = Vector4.Transform(_baseQuadPositions[i], transform);

            var quadVertex = new QuadVertex
            {
                Position = new Vector3(t.X, t.Y, t.Z),
                Color = new Vector4(color.R, color.G, color.B, color.A),
                TexCoord = defaultTextureCoords[i],
                TexIndex = 0.0f,
                TilingFactor = 1.0f
            };

            _quads.Add(quadVertex);
        }
    }
}
