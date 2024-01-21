using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Maths;
using System.Numerics;

using static SharpStone.Application;

namespace HelloWorld.Layers;
internal class SquareDemo() : Layer("Square Demo")
{
    private VertexArray _vba;
    private Shader _shader;

    private float r = 0.1f;
    private float increment = 1f;

    public override void OnAttach()
    {
        Vector2[] positions = [
            new(-0.5f, -0.5f),
            new(0.5f, -0.5f),
            new(0.5f, 0.5f),
            new(-0.5f, 0.5f)
        ];

        uint[] indeces = [
            0,1,2,
            2,3,0
        ];

        _vba = VertexArray.Create();
        var vbo = VertexBuffer.Create(positions);
        var ib = IndexBuffer.Create(indeces);

        vbo.Layout.Add("index", ShaderDataType.Float2);

        _vba.AddVertexBuffer(vbo);
        _vba.SetIndexBuffer(ib);

        _shader = Shader.Create("default");
        _shader.Bind();
    }

    public override void OnUpdate(float v)
    {
        RenderCommand.SetViewPort(0, 0, Window.Width, Window.Height);
        RenderCommand.SetClearColor(Color.Blue);
        RenderCommand.Clear();

        if (r > 1.0f)
        {
            increment = -0.05f;
        }
        else if (r < 0.0f)
        {
            increment = 0.05f;
        }

        r += increment;

        _shader.SetFloat4("u_Color", new(r, 0.0f, 0.0f, 1.0f));
        RenderCommand.DrawIndexed(_vba);
    }
}
