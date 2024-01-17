using SharpStone.Events;

namespace SharpStone.Events;

public record Event
{
    internal bool Handled = false;
    public virtual EventType EventType { get; }

    public virtual string Name { get { return GetType().Name; } }
    public virtual EventCategory GategoryFlags { get; }

    public override string ToString()
    {
        return Name;
    }

    public bool IsInCategory(EventCategory category)
    {
        return GategoryFlags == category;
    }

}
