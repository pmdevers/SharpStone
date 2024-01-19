namespace SharpStone.Events;
public record WindowResizedEvent(int Width, int Height) : Event
{
    public override string ToString()
    {
        return $"{Name}: {Width}, {Height}";
    }

    public override EventCategory GategoryFlags => EventCategory.Application;
}
