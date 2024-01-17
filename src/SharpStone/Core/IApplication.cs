namespace SharpStone.Core;

public interface IApplication
{
    IWindow Window { get; }
    ILogger Logger { get; }

    ILayerStack LayerStack { get; }

    static abstract IApplicationBuilder Create();
}

public interface ISystem
{
    bool Initialize(IApplication app);
    void Update();
}
