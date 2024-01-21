using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui;
using SharpStone.Maths;
using System.Numerics;

namespace HelloWorld.Layers;

internal class MainLayer : Layer
{
    public MainLayer() : base("Sandbox Main Layer")
    {
        UserInterface.Add(new TestElement()
        {
            Position = new Vector2(-0.5f, -0.5f),
            Size = new(1, 1),
        });
    }

    public override void OnUpdate(float v)
    {
        RenderCommand.SetViewPort(0, 0, Window.Width, Window.Height);
        RenderCommand.SetClearColor(Color.Blue);
        RenderCommand.Clear();
    }

    public override void OnEvent(Event @event)
    {
        var dispatcher = new EventDispatcher(@event);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPress);
    }

    public bool OnKeyPress(KeyPressedEvent e)
    {
        

        //if (e.KeyCode == 97 )
        //{
        //    _camera.Position += new Vector3(-0.1f, 0f, 0f);
        //}
        //if (e.KeyCode == 119)
        //{
        //    _camera.Position += new Vector3(0.0f, 0.1f, 0f);
        //}
        //if (e.KeyCode == 115)
        //{
        //    _camera.Position += new Vector3(0.0f, -0.1f, 0f);
        //}
        //if (e.KeyCode == 100)
        //{
        //    _camera.Position += new Vector3(0.1f, 0f, 0f);
        //}

        //if(e.keyCode == 110)
        //{
        //    _camera.Rotation += 1f;
        //}

        //if (e.keyCode == 109)
        //{
        //    _camera.Rotation -= 1f;
        //}


        return false;
    }
}
