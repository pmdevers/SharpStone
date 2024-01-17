using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Setup;
using static SharpStone.Logging;

namespace SharpStone;

public sealed class Application(IConfigurationManager config, IServiceRegistery services)
{
    public IConfigurationManager Config { get; } = config;
    public IServiceRegistery Services { get; } = services;
    public IResourceManager Resources { get; } = services.GetService<IResourceManager>();
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
        IsRunning = false;
        return true;
    }

    public static IApplicationBuilder Create(params object[] args)
    {
        return new ApplicationBuilder(args);
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
}
