using SharpStone.Events;

namespace SharpStone.Core;
public interface ILayerStack : IEnumerable<Layer>
{
    void OnEvent(Event e);
    void PushLayer(Layer layer);
    void PopLayer(Layer layer);
    void PushOverlay(Layer layer);
    void PopOverlay(Layer overlay);
}

public abstract class Layer(string name)
{
    public string Name { get; } = name;
    public virtual void OnEvent(Event @event) { }
    public abstract void OnUpdate(float v);
    public virtual void OnAttach() { }
    public virtual void OnDetach() { }

    public virtual void OnGuiRender() { }
}
