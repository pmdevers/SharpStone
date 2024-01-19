using SharpStone.Core;
using SharpStone.Events;
using System.Collections;

using static SharpStone.Logging;

namespace SharpStone.Layers;

public class LayerStack : ILayerStack
{
    private int _layerIndex = 0;
    private readonly List<Layer> _layers = [];

    public void PushLayer(Layer layer) 
    {
        _layers.Insert(_layerIndex, layer);
        _layerIndex++;
        Logger.Debug<LayerStack>($"Layer: {layer.Name} pushed!");
    }

    public void PushOverlay(Layer layer)
    {
        _layers.Add(layer);
        Logger.Debug<LayerStack>($"Overlay: {layer.Name} pushed!");
    }

    public void PopLayer(Layer layer)
    {
        _layers.Remove(layer);
        _layerIndex--;
        Logger.Debug<LayerStack>($"Layer {layer.Name} removed!");
    }

    public void PopOverlay(Layer layer)
    {
        _layers.Remove(layer);
        Logger.Debug<LayerStack>($"Overlay {layer.Name} removed!");
    }

    public void OnEvent(Event e)
    {
        for (var i = _layers.Count - 1; i >= 0; i--)
        {
            _layers[i].OnEvent(e);
            if (e.Handled)
                break;
        }
    }

    public IEnumerator<Layer> GetEnumerator()
        => _layers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}