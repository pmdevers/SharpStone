using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Gui.Controls;
using System.Numerics;
using static SharpStone.Logging;

namespace SharpStone.Gui;
internal class UserInterface : IUserInterface
{
    private static uint _uniqueId = 0;
    private Matrix4x4 _projectionMatix;
    public ControlContainer Container { get; private set; }
    public bool Visible { get; set; }

    internal static IUserInterface Create()
    {
        var ui = new UserInterface();
        
        Logger.Assert<UserInterface>(ui.Init(), "Failed to initialize UserInterface.");

        return ui;
    }

    public UserInterface()
    {
        _projectionMatix = Matrix4x4.CreateTranslation(new Vector3(-Application.Window.Width / 2f, -Application.Window.Height / 2f, 0))
            * Matrix4x4.CreateOrthographic(Application.Window.Width, Application.Window.Height, 0, 1000);

        Container = new ControlContainer(0, 0, Application.Window.Width, Application.Window.Height);
        Visible = true;
    }

    public void Add(BaseControl control)
    {
        Container.Controls.Add(control);
    }

    public bool Init()
    {
        return true;
    }
    public void Update()
    {
        if(!Visible) return;

        Container.Update();

        //Application.Renderer.Renderer2D.BeginScene(null, _projectionMatix);
        Container.Draw();
        //Application.Renderer.Renderer2D.EndScene();
    }

    public bool Shutdown()
    {
        return true;
    }

    public void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizedEvent>(OnWindowResized);
    }

    private bool OnWindowResized(WindowResizedEvent @event)
    {
        _projectionMatix = _projectionMatix = Matrix4x4.CreateTranslation(new Vector3(-Application.Window.Width / 2f, -Application.Window.Height / 2f, 0))
            * Matrix4x4.CreateOrthographic(Application.Window.Width, Application.Window.Height, 0, 1000);

        return false;
    }

    public static uint GetUniqueId() => _uniqueId++;

    
}
