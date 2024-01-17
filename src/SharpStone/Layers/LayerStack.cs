using SharpStone.Core;
using SharpStone.Events;

using static SharpStone.Logging;

namespace SharpStone.Layers;

public class LayerStack : ILayerStack
{
    private Application _app;
    private int _layerIndex = 0;
    private readonly List<ILayer> _layers = [];

    public void PushLayer(ILayer layer) 
    {
        _layers.Insert(_layerIndex,layer);
        _layerIndex++;
    }

    public void PushOverlay(ILayer layer)
    {
        _layers.Add(layer);
    }

    public void PopLayer(ILayer layer)
    {
        _layers.Remove(layer);
        _layerIndex--;
    }

    public void PopOverlay(ILayer overlay)
    {
        _layers.Remove(overlay);
    }

    public bool Init(Application app) 
    {
        foreach (var layer in _layers)
        {
            Logger.Debug<LayerStack>($"Initialize layer: {layer.Name}");
            var ok = layer.Init(app);
            Logger.Assert<LayerStack>(ok, $"Layer: {layer.Name} Initialize failed!.");
        }

        return true;
    }

    public void Update()
    {
        foreach (var layer in _layers)
        {
            layer.Update();
        }
    }

    public bool Shutdown() 
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
        for (var i = _layers.Count - 1; i != 0; i--)
        {
            _layers[i].OnEvent(e);
            if (e.Handled)
                break;
        }
    }
}