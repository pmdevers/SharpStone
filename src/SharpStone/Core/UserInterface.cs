using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui;

namespace SharpStone.Core;
public class UserInterface
{
    private static uint _uniqueId = 0;
    public static bool Visible { get; set; }
    public static OrthographicCamera Camera { get; private set; } = new OrthographicCamera(-1f, 1f, -1f, 1f);

    public static ControlContainer Container { get; private set; }

    internal static bool Init()
    {
        Container = new ControlContainer(0, Window.Width, Window.Height, 0);
        Camera = new OrthographicCamera(-1f, 1f, -1f, 1f);
        Visible = true;
        return true;
    }

    public static void Add(BaseControl control)
    {
        Container.Controls.Add(control);
    }
    public static void Update()
    {
        if (!Visible) return;
        Container.Update();

        Renderer.BeginScene(Camera);
        Container.Draw();
        Renderer.EndScene();
    }

    public static bool Shutdown()
    {
        return true;
    }

    public static void OnEvent(Event e)
    {
        if (!Visible) return;
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizedEvent>(OnWindowResized);
    }

    private static bool OnWindowResized(WindowResizedEvent @event)
    {
        
        return false;
    }

    public static uint GetUniqueId() => _uniqueId++;
}
