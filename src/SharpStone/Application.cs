using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui;
using SharpStone.Layers;
using System.Diagnostics;
using System.Reflection;

namespace SharpStone;

public class ApplicationConfig()
{
    public string Name { get;set; }
    public Assembly AssetsAssembly { get; set; } = Assembly.GetEntryAssembly();
    public int Width { get; set; } = 1280;
    public int Height { get; set; } = 720;
}

public class Application
{
    private const float LOW_LIMIT = 0.0167f;
    private const float HIGH_LIMIT = 0.1f;

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

        Window.Init(new WindowArgs(applicationConfig.Name, applicationConfig.Width, applicationConfig.Height));
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

        UserInterface.OnEvent(e);
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

        if (_layers.Count() == 0)
        {
            PushLayer(new DebugLayer());
        }


        var sw = Stopwatch.StartNew();
        var lastTime = sw.ElapsedMilliseconds;

        while (IsRunning)
        {
            var currentTime = sw.ElapsedMilliseconds;
            var deltaTime = currentTime - lastTime / 1000f;
            if (deltaTime < LOW_LIMIT)
                deltaTime = LOW_LIMIT;
            else if (deltaTime > HIGH_LIMIT)
                deltaTime = HIGH_LIMIT;

            lastTime = currentTime;

            foreach (var layer in _layers)
            {
                layer.OnUpdate(deltaTime);
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
