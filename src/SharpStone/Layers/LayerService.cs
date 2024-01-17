using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Renderer;
using static SharpStone.Logging;

namespace SharpStone.Layers;
internal class LayerService(ILayerStack layers) : IService
{
    private IRenderApi _renderApi;

    public bool Init(Application app)
    {
        _renderApi = app.Services.GetService<IRenderApi>();

        foreach (var layer in layers)
        {
            Logger.Debug<LayerStack>($"Initialize layer: {layer.Name}");
            var ok = layer.Init(_renderApi);
            Logger.Assert<LayerStack>(ok, $"Layer: {layer.Name} Initialize failed!.");
        }

        return true;
    }

    public void Update()
    {
        foreach (var layer in layers)
        {
            layer.Update(_renderApi);
        }
    }

    public bool Shutdown(Application app)
    {
        foreach (var layer in layers)
        {
            var ok = layer.Shutdown();
            Logger.Assert<LayerStack>(ok, $"Layer: {layer.Name} Shutdown failed!.");
        }

        return true;
    }

    public void OnEvent(Event e)
    {
        foreach (var layer in layers)
        {
            layer.OnEvent(e);
        }
    }
}
