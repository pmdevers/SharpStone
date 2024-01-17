using SharpStone.Events;

namespace SharpStone.Core;
public interface ILayerStack
{
    bool Init(Application app);
    void Update();    
    bool Shutdown();
    void OnEvent(Event e);
    void PushLayer(ILayer layer);
    void PopLayer(ILayer layer);

    void PushOverlay(ILayer layer);
    void PopOverlay(ILayer overlay);
}

public interface ILayer<TSelf> : ILayer
    where TSelf : ILayer
{
    static abstract TSelf Create();
}

public interface ILayer
{
    public string Name { get; }

    virtual bool Init(Application app) => true;
    virtual bool Shutdown() => true;
    virtual void Update() { }

    virtual void OnEvent(Event @event) { }
}
