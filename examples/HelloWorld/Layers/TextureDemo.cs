using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Platform.Win32.D3D12;
using System.Numerics;

namespace HelloWorld.Layers;
public unsafe class TextureDemo() : Layer("Texture Demo")
{
    private Shader _shader;
    private Texture2D _texture;
    private VertexArray _vba;

    private struct VertextData(float[] f)
    {
        public Vector3 Position = new(f[0], f[1], f[2]);
        public Vector3 Color = new(f[3], f[4], f[5]);
        public Vector2 Coords = new(f[6], f[7]);
    }

    public override void OnAttach()
    {
        _texture = Texture2D.Create("wall");

        VertextData[] vertices = {
            new ([-0.5f,  -0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f]),   // top right
            new ([0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f]),   // bottom right
            new ([0.5f, 0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f]),   // bottom left
            new ([-0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f])    // top left 
        };

        uint[] indecies = [
            0,1,2,
            2,3,0,
            ];

        _vba = VertexArray.Create();
        var vbo = VertexBuffer.Create(vertices);
        vbo.Layout.Add("positions", ShaderDataType.Float3);
        vbo.Layout.Add("color", ShaderDataType.Float3);
        vbo.Layout.Add("coords", ShaderDataType.Float2);

        var ibo = IndexBuffer.Create(indecies);

        _vba.SetIndexBuffer(ibo);
        _vba.AddVertexBuffer(vbo);

        _shader = Shader.Create("Texture");
        _shader.Bind();

        _texture.Bind(0);

    }
    public override void OnUpdate(float v)
    {
        _texture.Bind(0);
        _shader.Bind();
        RenderCommand.DrawArrays(_vba, 8);

    }
}
