using SharpStone.Configuration;
using SharpStone.Core;
using SharpStone.Layers;
using SharpStone.Resources;
using SharpStone.Services;
using SharpStone.Window;
using System.Runtime.CompilerServices;

namespace SharpStone.Setup;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder AddLayer<TLayer>(this IApplicationBuilder builder) 
        where TLayer : ILayer<TLayer>
        => builder.AddLayer(TLayer.Create());

    public static IApplicationBuilder WindowConfig(this IApplicationBuilder builder, Func<WindowArgs> config)
    {
        builder.Config(config());
        return builder;
    }
}

internal class ApplicationBuilder(object[] args) : IApplicationBuilder
{
    private readonly IWindow _window = new SDL2Window();
    private readonly LayerStack _layerStack = new();
    private readonly ResourceManager _resources = new();
    private readonly ConfigurationManager _config = new();
    private readonly ServiceRegistery _services = new();

    public IApplicationBuilder AddLayer(ILayer layer)
    {
        _layerStack.PushLayer(layer);
        return this;
    }

    public IApplicationBuilder Config<T>(T config)
    {
        _config.SetConfig(config);
        return this;
    }
    
    public IApplicationBuilder AddService(IService service)
    {
        _services.AddService(service);
        return this;
    }

    public Application Build()
    {
        AddService(new WindowService(_window));
        AddService(_resources);
        AddService(new LayerService(_layerStack));
        AddLayer(new DebugLayer());

        return new Application(
            _config,
            _services
        );
    }
}
