using SharpStone.Maths;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SharpStone.Graphics;
public class Renderer
{
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
        new Vector4(-0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(0.5f, 0.5f, 0.0f, 1.0f),
        new Vector4(-0.5f, 0.5f, 0.0f, 1.0f)
    ];

    public static bool Init()
    {
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

        _quadVertexArray.AddVertexBuffer(vbo1);
        _quadVertexArray.SetIndexBuffer(ib);

        _quadShader = Shader.Create("UISolid");
        _quadShader.Bind();

        return true;
    }

    public static void BeginScene(Camera camera)
    {
        StartBatch();
    }
    public static void BeginScene(Camera camera, Matrix4x4 transform)
    {
        Matrix4x4.Invert(transform, out var inverse);
        var viewProjection = camera.GetViewProjection() * inverse;

        StartBatch();

        //CameraBuffer.ViewProjection = camera.GetProjection() * Inverse(transform)
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

            _quadShader.Bind();
            
            RenderCommand.DrawIndexed(_quadVertexArray);
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
    public struct QuadVertex(Vector3 position, Vector4 color)
    {
        public Vector3 Position = position;
        public Vector4 Color = color;
    }
    public static void DrawQuad(Vector2 position, Vector2 size, Color color)
    {
        var v3Position = new Vector3(position, 1.0f);
        var v3Size = new Vector3(size, 1.0f);

        var translation = Matrix4x4.CreateTranslation(v3Position);
        var scaling = Matrix4x4.CreateScale(v3Size);

        var transform = scaling * translation;

        for (int i = 0; i < _baseQuadPositions.Length; i++)
        {
            var t = Vector4.Transform(_baseQuadPositions[i], transform);
            var quadVertex = new QuadVertex
            {
                Position = new Vector3(t.X, t.Y, t.Z),
                Color = new Vector4(color.R, color.G, color.B, color.A),
            };

            _quads.Add(quadVertex);
        }
    }
}
