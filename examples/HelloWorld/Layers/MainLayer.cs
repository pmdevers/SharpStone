using SharpStone.Core;
using SharpStone.Rendering;
using static SharpStone.Application;

namespace HelloWorld.Layers;

internal class MainLayer : Layer
{
    private IVertexArray _vertexArray;

    public MainLayer() : base("Sandbox Main Layer")
    {
        _vertexArray = Renderer.Factory.CreateVertexArray();
    }

    public override void OnUpdate(float v)
    {
    }
}
