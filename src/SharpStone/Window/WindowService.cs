using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Setup;
using static SharpStone.Logging;

namespace SharpStone.Window;
internal class WindowService : IService
{
    private IWindow? _window;
    public bool Init(Application app)
    {
        var api = app.Config.GetConfig<RenderApi>();

        _window = api switch { 
            RenderApi.OpenGL => new SDL2Window(), 
            _ => null 
        };

        if(_window == null)
        {
            Logger.Fatal<WindowService>($"Renderer '{api}' not supported!.");
            return false;
        }

        var config = app.Config.GetConfig<WindowArgs>();
        var result = _window.Init(config, app.OnEvent);

        Logger.Assert<WindowService>(result, $"Failed to initialize a window.");

        return result;
    }

    public void OnEvent(Event e)
    {
    }

    public bool Shutdown(Application app)
    {
        return _window.Shutdown();
    }

    public void Update()
    {
        _window?.Update();
    }
}
