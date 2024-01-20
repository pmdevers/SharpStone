using SharpStone.Maths;
using System.Numerics;
using System.Runtime.InteropServices;
using static SharpStone.Application;

namespace SharpStone.Rendering.OpenGL;

internal unsafe class OpenGLRenderer2D : IRenderer2D
{
    public const int MaxQuads = 20000;
    public const int VerticesPerQuad = 4;
    public const int IndicesPerQuad = 6;

    public const int MaxVertices = MaxQuads * VerticesPerQuad;
    public const int MaxIndices = MaxQuads * IndicesPerQuad;

    private IVertexArray _vertexArray1;
    private IShader _shader;

    private readonly List<QuadVertex> _quads = new(MaxVertices);
    private readonly uint[] _quadIndices = new uint[MaxIndices];

    private readonly Vector4[] _baseQuadPositions = [
        new Vector4(-0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4(0.5f, 0.5f, 0.0f, 1.0f),
        new Vector4(-0.5f, 0.5f, 0.0f, 1.0f)
    ];

    public bool Init()
    {
        _vertexArray1 = Renderer.Factory.CreateVertexArray();

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

        var vbo1 = Renderer.Factory.CreateVertexBuffer<QuadVertex>(MaxVertices);
        var ib = Renderer.Factory.CreateIndexBuffer(_quadIndices);

        vbo1.Layout.Add("positions", ShaderDataType.Float3);
        vbo1.Layout.Add("colors", ShaderDataType.Float4);

        _vertexArray1.AddVertexBuffer(vbo1);
        _vertexArray1.SetIndexBuffer(ib);

        _shader = Renderer.Factory.CreateShader("UISolid");
        _shader.Bind();

        return true;
    }

    public bool Shutdown() => true;

    public void BeginScene(ICamera camera, Matrix4x4 transform)
    {
        _shader.Bind();
        _shader.SetMatrix4("u_Matrix", transform);
        Renderer.Commands.DrawIndexed(_vertexArray1);
    }

    public void EndScene()
    {
        
    }

    public void Flush()
    {
        
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuadVertex(Vector3 position, Vector4 color)
    {
        public Vector3 Position = position;
        public Vector4 Color = color;
    }
    public void DrawQuad(Vector2 position, Vector2 size, Color color)
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
