using SharpStone.Core;
using SharpStone.Events;
using static SharpStone.Logging;

public class EventDispatcher
{
    private readonly Event _event;

    public EventDispatcher(Event @event)
    {
        _event = @event;
    }

    public bool Dispatch<T>(Func<T, bool> func)
        where T : Event
    {
        if (_event.GetType() == typeof(T))
        {
            _event.Handled = func((T)_event);
            Logger.Assert<EventDispatcher>(!_event.Handled, $"Event: {_event.Name} - {_event.GategoryFlags}.");

            return true;
        }
        return false;
    }
}

public delegate void EventCallback(Event e);
