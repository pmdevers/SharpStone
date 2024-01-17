using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Layers;
using SharpStone.Renderer;
using SharpStone.Resources;
using SharpStone.Setup;
using SharpStone.Window;
using static SharpStone.Logging;

namespace SharpStone;

public sealed class Application(IConfigurationManager config, IServiceRegistery services)
{
    public IConfigurationManager Config { get; } = config;
    public IServiceRegistery Services { get; } = services;
    public IResourceManager Resources { get; } = services.GetService<ResourceManager>();
    public bool IsRunning { get; private set; }

    private bool Init()
    {
        var ok = services.Init(this);
        Logger.Assert<Application>(ok, $"Failed to initialize layerstack.");
        return ok;
    }

    public void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClosed);
        services.OnEvent(e);
    }

    private bool OnWindowClosed(WindowCloseEvent @event)
    {
        Close();
        return true;
    }

    public static IApplicationBuilder Create(params object[] args)
    {
        var builder =  new ApplicationBuilder(args);
        
        builder.Config(RenderApi.OpenGL);

        builder.AddService(new WindowService());
        builder.AddService(new RenderService());
        builder.AddService(new ResourceManager());
        builder.AddService(new LayerService());

        builder.AddLayer(new DebugLayer());

        return builder;
    }

    public void Run()
    {
        IsRunning = Init();        

        Logger.Info<Application>("Main Loop started.");

        while (IsRunning)
        {
            services.Update();
        }

        services.Shutdown(this);
    }

    public void Close()
    {
        IsRunning = false;
    }
}
