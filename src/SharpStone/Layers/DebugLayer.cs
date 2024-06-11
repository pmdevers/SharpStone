using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Gui;
using SharpStone.Gui.Controls;
using SharpStone.Maths;

namespace SharpStone.Layers;

internal unsafe class DebugLayer() : Layer("Debug Layer")
{
    Camera _camera;
    Texture2D _texture;
    public override void OnAttach()
    {
        _camera =  new Camera();
        _texture = Texture2D.Create("sharpstone");

        UserInterface.Visible = true;

        UserInterface.Add(new TestElement()
        {
            Position = new(0f, 0f),
            Size = new(100, 100),
        });
    }

    public override void OnUpdate(float v)
    {
       
        Renderer.BeginScene(_camera);

        Renderer.DrawQuad(new(0f, 0f), new(2f,2f), _texture, 1, Color.White);

        Renderer.EndScene();

    }
}
