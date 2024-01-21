namespace SharpStone.Events;
public abstract record KeyEvent(int keyCode) : Event
{
    public int KeyCode { get; } = keyCode;
    public override EventCategory GategoryFlags
    {
        get
        {
            return EventCategory.Keyboard | EventCategory.Input;
        }
    }
}

public record KeyPressedEvent(int keyCode, int repeatCount) : KeyEvent(keyCode)
{
    public int RepeatCount { get; } = repeatCount;

    public override string ToString()
    {
        return $"{Name}: {KeyCode} ({RepeatCount} repeats)";
    }
}

public record KeyReleaseEvent(int keyCode) : KeyEvent(keyCode)
{
    public override string ToString()
    {

        return $"{Name}: {KeyCode}";
    }

}
