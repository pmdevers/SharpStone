using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Maths;
using System.Numerics;

namespace HelloWorld.Layers;

internal class MainLayer : Layer
{
    private Camera _camera = new Camera();
    public MainLayer() : base("Sandbox Main Layer")
    {
        //UI.Add(new TestElement());


    }

    public override void OnUpdate(float v)
    {
        RenderCommand.SetClearColor(Color.CornflowerBlue);
        RenderCommand.Clear();

        Renderer.BeginScene(_camera);

        Renderer.DrawQuad(new Vector2(-0.5f, 0.5f), new Vector2(1f, 1f), Color.Green);

        Renderer.EndScene();
    }
}
