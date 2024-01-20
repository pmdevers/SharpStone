using SharpStone.Configuration;
using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Gui;
using SharpStone.Layers;
using SharpStone.Rendering;
using SharpStone.Resources;
using SharpStone.Window;
using System.Reflection;
using static SharpStone.Logging;

namespace SharpStone;

public struct ApplicationConfig()
{
    public string Name { get;set; }
    public RenderApi RenderApi { get; set; } = RenderApi.OpenGL;
    public Assembly AssetsAssembly { get; set; } = Assembly.GetEntryAssembly();
}

public class Application
{
    private static Application? _instance;
    public static Application Instance => _instance ?? throw new InvalidOperationException();
    public static IWindow Window => Instance._window;
    public static IRenderApi Renderer => Instance._renderApi;
    public static IConfigurationManager Config => Instance._config;
    public static IResourceManager ResourcesManager => Instance._resources;
    public static IUserInterface UI => Instance._userInterface;
    
    private readonly IWindow _window;
    private readonly IRenderApi _renderApi;
    private readonly IConfigurationManager _config;
    private readonly IResourceManager _resources;
    private readonly ILayerStack _layers;
    private readonly IUserInterface _userInterface;

    public static RenderApi Api => RenderApi.OpenGL;
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
        _window = WindowService.Create(new WindowArgs(applicationConfig.Name));
        _renderApi = RenderService.Create(applicationConfig.RenderApi);
        _config = new ConfigurationManager();
        _resources = new ResourceManager(applicationConfig.AssetsAssembly);
        _layers = new LayerStack();
        _userInterface = UserInterface.Create();

        _renderApi.Renderer2D.Init();
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

            _userInterface.Update();
            _window.Update();
        }

        _renderApi.Renderer2D.Shutdown();
        _window.Shutdown();
    }

    public void Close()
    {
        IsRunning = false;
    }
}
