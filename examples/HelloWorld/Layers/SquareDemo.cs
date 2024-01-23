using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Maths;
using System.Numerics;

namespace HelloWorld.Layers;
internal class SquareDemo() : Layer("Square Demo")
{
    private Camera _camera = new OrthographicCamera(-3f, 3f, -3f, 3f);
    private readonly VertexArray _vba = VertexArray.Create();
    private readonly Shader _shader = Shader.Create("default");

    private float _r = 0.1f;
    private float _increment = 1f;

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

        var vbo = VertexBuffer.Create(positions);
        var ib = IndexBuffer.Create(indeces);

        vbo.Layout.Add("index", ShaderDataType.Float2);

        _vba.AddVertexBuffer(vbo);
        _vba.SetIndexBuffer(ib);

        _shader.Bind();
    }

    public override void OnUpdate(float v)
    {
        RenderCommand.SetViewPort(0, 0, Window.Width, Window.Height);
        RenderCommand.SetClearColor(Color.Blue);
        RenderCommand.Clear();

        if (_r > 1.0f)
        {
            _increment = -0.05f;
        }
        else if (_r < 0.0f)
        {
            _increment = 0.05f;
        }

        _r += _increment;

        _shader.Bind();
        _shader.SetFloat4("u_Color", new(_r, 0.0f, 0.0f, 1.0f));
        _shader.SetMatrix4("u_ViewProjection", _camera.ProjectionView);

        RenderCommand.DrawIndexed(_vba);


        Renderer.BeginScene(_camera);

        Renderer.DrawQuad(new(0, 0), new(1f, 1f), Color.Green);

        Renderer.EndScene();


    }
}
