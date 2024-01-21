using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using System.Reflection;

namespace SharpStone;

public struct ApplicationConfig()
{
    public string Name { get;set; }
    public Assembly AssetsAssembly { get; set; } = Assembly.GetEntryAssembly();
}

public class Application
{
    private static Application? _instance;
    public static Application Instance => _instance ?? throw new InvalidOperationException();

    private readonly LayerStack _layers = new();

    public bool IsRunning { get; private set; }
    public bool IsMinimized { get; private set; }
    
    public static Application Create(Action<ApplicationConfig> config)
    {
        var cfg = new ApplicationConfig();
        config(cfg);
        return new(cfg);
    }

    public Application(ApplicationConfig applicationConfig)
    {
        Logger.Assert<Application>(_instance == null, "Application was already running.");
        _instance = this;

        Window.Init(new WindowArgs(applicationConfig.Name));
        RenderCommand.Init();
        Renderer.Init();
        UserInterface.Init();
    }

    public Application PushLayer(Layer layer)
    {
        _layers.PushLayer(layer);
        layer.OnAttach();
        return this;
    }

    public Application PushOverlay(Layer layer)
    {
        _layers.PushOverlay(layer);
        layer.OnAttach();
        return this;
    }

    public void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClosed);
        dispatcher.Dispatch<WindowResizedEvent>(OnWindowResized);

        Logger.Trace<Application>($"Event Raised: {e.Name}.");

        _layers.OnEvent(e);
    }

    private bool OnWindowResized(WindowResizedEvent @event)
    {
        if(@event.Width == 0 || @event.Height == 0)
        {
            IsMinimized = true;
            return false;
        }

        IsMinimized = true;
        return false;
    }

    private bool OnWindowClosed(WindowCloseEvent @event)
    {
        Close();
        return true;
    }

    public void Run()
    {
        IsRunning = true;
        Logger.Info<Application>("Main Loop started.");

        while (IsRunning)
        {
            foreach (var layer in _layers)
            {
                layer.OnUpdate(0f);
            }

            UserInterface.Update();
            Window.Update();
        }

        Window.Shutdown();
    }

    public void Close()
    {
        IsRunning = false;
    }
}
