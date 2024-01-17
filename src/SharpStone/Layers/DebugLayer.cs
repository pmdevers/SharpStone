using SharpStone.Core;
using SharpStone.Renderer;
using SharpStone.Maths;
using System.Runtime.CompilerServices;

namespace SharpStone.Layers;

internal unsafe class DebugLayer : ILayer<DebugLayer>
{
    public string Name => "DebugLayer";

    private IVertexArray _vba;
    
    public static DebugLayer Create()
    {
        return new DebugLayer();
    }

    public bool Init(IRenderApi renderApi)
    {
        float[] positions = [
            -0.5f, -0.5f,
             0.5f, -0.5f,
             0.5f,  0.5f,
            -0.5f,  0.5f
        ];

        uint[] indices = [
            0, 1, 2,
            2, 3, 0
        ];

        _vba = renderApi.Factory.CreateVertexArray();
        var vertexBuffer = renderApi.Factory.CreateVertexBuffer(positions, 2);
        var indexBuffer = renderApi.Factory.CreateIndexBuffer(indices, 1);

        vertexBuffer.Layout.Add(
            new BufferElement() { 
                Name = "Index", 
                Type = ShaderDataType.Float, 
                Size = sizeof(float) * 2, 
                Offset = 0, 
                Normalized = false 
            });

        _vba.AddVertexBuffer(vertexBuffer);
        _vba.SetIndexBuffer(indexBuffer);

        var shader = renderApi.Factory.CreateShader("default", _vertShader, _fragmentShader);
        shader.Bind();      

        return true;
    }

    public void Update(IRenderApi renderApi)
    {
        renderApi.Commands.SetClearColor(Color.CornflowerBlue);
        renderApi.Commands.Clear();
        renderApi.Commands.DrawIndexed(_vba);
    }
         

    private readonly string _vertShader = @"
#version 330 core

layout(location = 0) in vec4 position;

void main() {
    gl_Position = position;
}
";

    private readonly string _fragmentShader = @"

#version 330 core

layout(location = 0) out vec4 color;

uniform vec4 u_Color;

void main() {
    color = vec4(1.0, 0.0, 0.0, 1.0);
}

";
}
