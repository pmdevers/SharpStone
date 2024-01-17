using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Renderer.OpenGL;
using SharpStone.Setup;
using static SharpStone.Logging;

namespace SharpStone.Renderer;
internal class RenderService : IService
{
    public IRenderApi Renderer { get; set; }

    public bool Init(Application app)
    {
        var config = app.Config.GetConfig<RenderApi>();
        var api = config switch
        {
            RenderApi.OpenGL => new OpenGLRenderer(),
            _ => null
        };

        if(api == null)
        {
            Logger.Error<RenderService>($"Renderer {config} Not Supported!");
            return false;
        }

        Renderer = api;

        return Renderer.Init(app);
    }

    public void OnEvent(Event e)
        => Renderer.OnEvent(e);
    
    public bool Shutdown(Application app) 
        => Renderer.Shutdown(app);

    public void Update()
        => Renderer.Update();
}
