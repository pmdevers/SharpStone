using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Renderer;
using static SharpStone.Logging;

namespace SharpStone.Layers;
internal class LayerService : IService
{
    private readonly ILayerStack _layers = new LayerStack();
    
    public ILayerStack Layers { get { return _layers; } }
    public IRenderApi RenderApi { get; set; }

    public bool Init(Application app)
    {
        RenderApi = app.Services.GetService<RenderService>().Renderer;

        foreach (var layer in _layers)
        {
            Logger.Debug<LayerStack>($"Initialize layer: {layer.Name}");
            var ok = layer.Init(RenderApi);
            Logger.Assert<LayerStack>(ok, $"Layer: {layer.Name} Initialize failed!.");
        }

        return true;
    }

    public void Update()
    {
        foreach (var layer in _layers)
        {
            layer.Update(RenderApi);
        }
    }

    public bool Shutdown(Application app)
    {
        foreach (var layer in _layers)
        {
            var ok = layer.Shutdown();
            Logger.Assert<LayerStack>(ok, $"Layer: {layer.Name} Shutdown failed!.");
        }

        return true;
    }

    public void OnEvent(Event e)
    {
        foreach (var layer in _layers)
        {
            layer.OnEvent(e);
        }
    }
}
