using SharpStone.Events;

namespace SharpStone.Core;

public abstract class Layer(string name)
{
    public string Name { get; } = name;
    public virtual void OnEvent(Event @event) { }
    public abstract void OnUpdate(float v);
    public virtual void OnAttach() { }
    public virtual void OnDetach() { }

    public virtual void OnGuiRender() { }
}
