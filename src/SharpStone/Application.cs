using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Layers;
using SharpStone.Platform.OpenGL;
using SharpStone.Setup;
using static SharpStone.Logging;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone;

public sealed class Application
{
    public static Application? Instance { get; private set; }

    public IWindow Window { get; }
    public ILayerStack LayerStack { get; }

    public IResourceManager Resources { get; }
    public bool IsRunning { get; private set; }


    internal Application(IWindow window, ILayerStack layerStack, IResourceManager resourceManager)
    {
        Logger.Assert<Application>(Instance != null, "Application already registered.");
        Instance = this;

        if (!window.Init(new WindowArgs { Title = "Test", Width = 1280, Height = 720 }, OnEvent))
        {
            Logger.Error<Application>($"Failed to initialize a window.");
        };

        Window = window;

        if (!layerStack.Init(this))
        {
            Logger.Error<Application>($"Failed to initialize layerstack.");
        }

        if (!resourceManager.Init(this))
        {
            Logger.Error<Application>($"Failed to initialize resource manager.");
        }

        LayerStack = layerStack;
        Resources = resourceManager;
    }

    private void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClosed);
        LayerStack.OnEvent(e);
    }

    private bool OnWindowClosed(WindowCloseEvent @event)
    {
        IsRunning = false;
        return true;
    }

    public static IApplicationBuilder Create(params object[] args)
    {
        return new ApplicationBuilder()
            .AddLayer<DebugLayer>();
    }

    public void Run()
    {
        IsRunning = true;

        while (IsRunning)
        {
            LayerStack.Update();
            Window.Update();
        }

        Logger.Assert<Application>(LayerStack.Shutdown(), "Failed to Shutdown LayerStack.");
        Logger.Assert<Application>(Window.Shutdown(), "Failed to Shutdown window.");
        
    }
}
