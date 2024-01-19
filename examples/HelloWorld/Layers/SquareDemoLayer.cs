﻿using SharpStone.Core;
using SharpStone.Maths;
using SharpStone.Rendering;
using static SharpStone.Application;

namespace HelloWorld.Layers;
internal class SquareDemoLayer : Layer
{
    private IVertexArray _vba;
    private IShader _shader;

    float r = 0.1f;
    float increment = 1f;

    public SquareDemoLayer() : base("Square Deme Layer")
    {
        Vector2[] positions = [
            new(-0.5f, -0.5f),
            new(0.5f,  -0.5f),
            new(0.5f,   0.5f),
            new(-0.5f,  0.5f)
        ];

        uint[] indices = [
            0,
            1,
            2,
            2,
            3,
            0
        ];

        _vba = Renderer.Factory.CreateVertexArray();
        var vertexBuffer = Renderer.Factory.CreateVertexBuffer(positions);
        var indexBuffer = Renderer.Factory.CreateIndexBuffer(indices);

        vertexBuffer.Layout.Add("Index", ShaderDataType.Float2);

        _vba.AddVertexBuffer(vertexBuffer);
        _vba.SetIndexBuffer(indexBuffer);

        _shader = Renderer.Factory.CreateShader("default");
        _shader.Bind();
    }

    public override void OnUpdate(float v)
    {
        Renderer.Commands.SetViewPort(0, 0, Window.Width, Window.Height);
        Renderer.Commands.SetClearColor(Color.CornflowerBlue);
        Renderer.Commands.Clear();

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

        Renderer.Commands.DrawIndexed(_vba);
    }
}