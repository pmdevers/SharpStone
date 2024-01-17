using SharpStone.Core;
using SharpStone.Renderer;
using SharpStone.Maths;

namespace SharpStone.Layers;

internal unsafe class DebugLayer : ILayer<DebugLayer>
{
    public string Name => "DebugLayer";

    private IVertexArray vba;

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

        vba = renderApi.CreateVertexArray();
        var vertexBuffer = renderApi.CreateVertexBuffer(positions, 2);
        var indexBuffer = renderApi.CreateIndexBuffer(indices, 1);

        vertexBuffer.Layout = new BufferLayout([ new BufferElement() {  Name = "Index", Type = ShaderDataType.Float, Size = sizeof(float) * 2, Offset = 0, Normalized = false }]);

        vba.AddVertexBuffer(vertexBuffer);
        vba.SetIndexBuffer(indexBuffer);

        var shader = renderApi.CreateShader("default", _vertShader, _fragmentShader);
        shader.Bind();      

        return true;
    }

    public void Update(IRenderApi renderApi)
    {
        renderApi.SetClearColor(Color.CornflowerBlue);
        renderApi.Clear();
        
        //renderApi.DrawArrays(vba, 3);
        renderApi.DrawIndexed(vba);
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
