namespace SharpStone.Events;
public sealed record WindowCloseEvent : Event
{
    public override EventType EventType => EventType.WindowClose;
    public override EventCategory GategoryFlags => EventCategory.Application;
}
