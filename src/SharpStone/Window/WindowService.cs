using SharpStone.Core;
using SharpStone.Events;

using static SharpStone.Logging;

namespace SharpStone.Window;
internal class WindowService(IWindow window) : IService
{
    public bool Init(Application app)
    {
        var config = app.Config.GetConfig<WindowArgs>();
        var result = window.Init(config, app.OnEvent);

        Logger.Assert<WindowService>(result, $"Failed to initialize a window.");

        return result;
    }

    public void OnEvent(Event e)
    {
    }

    public bool Shutdown(Application app)
    {
        return window.Shutdown();
    }

    public void Update()
    {
        window?.Update();
    }
}
