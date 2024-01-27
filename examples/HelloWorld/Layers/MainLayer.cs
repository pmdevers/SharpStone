using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui;
using SharpStone.Gui.Controls;
using SharpStone.Maths;
using System.Numerics;

namespace HelloWorld.Layers;

public class TestCamera : Camera
{
    public override Matrix4x4 ProjectionView => Matrix4x4.CreateTranslation(new Vector3(1, 1, 0));
}

internal class MainLayer : Layer
{
    private Camera _camera = new OrthographicCamera(0, Window.Width, Window.Height, 0);
    private Texture2D _texture = Texture2D.Create("wall");

    public MainLayer() : base("Sandbox Main Layer")
    {
        //UserInterface.Visible = false;

        //UserInterface.Add(new TestElement()
        //{
        //    Position = new Vector2(0, 0),
        //    Size = new(200, 200),
        //    RelativeTo = Corner.Center
        //});

        UserInterface.Add(new TestElement()
        {
            Position = new Vector2(0, 0),
            Size = new(100, 50),
            RelativeTo = Corner.Center,
            Transparency = 100,
        });
    }

    public override void OnUpdate(float v)
    {
        //RenderCommand.SetViewPort(0, 0, Window.Width, Window.Height);
        //RenderCommand.SetClearColor(Color.CornflowerBlue);
        //RenderCommand.Clear();

        //UserInterface.Clear();

        

        //Renderer.DrawQuad(new Vector2(40, 40), new(100, 50), _texture, 1, Color.Yellow, Matrix4x4.Identity);
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

    public override void OnAttach()
    {
        throw new NotImplementedException();
    }
}
