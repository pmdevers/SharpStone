using SharpStone.Core;
using SharpStone.Events;
using System.Collections;
using static SharpStone.Logging;

namespace SharpStone.Layers;

public class LayerStack : ILayerStack
{
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

    public void OnEvent(Event e)
    {
        for (var i = _layers.Count - 1; i != 0; i--)
        {
            _layers[i].OnEvent(e);
            if (e.Handled)
                break;
        }
    }

    public IEnumerator<ILayer> GetEnumerator()
        => _layers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}