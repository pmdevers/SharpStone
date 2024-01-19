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
    public static Application Instance;

    public static IWindow Window => Instance._window;
    public static IRenderApi Renderer => Instance._renderApi;
    public static IConfigurationManager Config => Instance._config;
    public static IResourceManager ResourcesManager => Instance._resources;
    
    private IWindow _window;
    private IRenderApi _renderApi;
    private IConfigurationManager _config;
    private IResourceManager _resources;
    private ILayerStack _layers;
    private GuiLayer _gui_layer;

    public bool IsRunning { get; private set; }
    public bool IsMinimized { get; private set; }

    public Application(ApplicationConfig applicationConfig)
    {
        Logger.Assert<Application>(Instance == null, "Application was already running.");
        
        Instance = this;
        
        _window = WindowService.Create(new WindowArgs(applicationConfig.Name));
        _renderApi = RenderService.Create(applicationConfig.RenderApi);
        _config = new ConfigurationManager();
        _resources = new ResourceManager(applicationConfig.AssetsAssembly);
        _layers = new LayerStack();

        //_layers.PushLayer(new DebugLayer());

        //_gui_layer = new GuiLayer();
        //_layers.PushOverlay(_gui_layer);

        //Renderer.Renderer2D.Init();
    }

    public void PushLayer(Layer layer)
    {
        _layers.PushLayer(layer);
        layer.OnAttach();
    }

    public void PushOverlay(Layer layer)
    {
        _layers.PushOverlay(layer);
        layer.OnAttach();
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

            //_gui_layer.Begin();
            //foreach (var layer in _layers)
            //    layer.OnGuiRender();
            //_gui_layer.End();

            _window.Update();
        }
    }

    public void Close()
    {
        IsRunning = false;
    }
}
