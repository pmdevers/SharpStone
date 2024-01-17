using SharpStone.Events;

namespace SharpStone.Core;

public interface IWindow
{
    string Title { get; }
    int Width { get; }
    int Height { get; }
    bool Fullscreen { get; }

    bool Init(WindowArgs args, EventCallback eventCallback);
    bool Shutdown();
    void Update();
}