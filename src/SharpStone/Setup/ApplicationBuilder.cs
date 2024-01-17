using SharpStone.Configuration;
using SharpStone.Core;
using SharpStone.Layers;
using SharpStone.Services;

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
    private readonly ConfigurationManager _config = new();
    private readonly ServiceRegistery _services = new();

    public IApplicationBuilder AddLayer(ILayer layer)
    {
        var layerService = _services.GetService<LayerService>();
        layerService.Layers.PushLayer(layer);
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
        return new Application(
            _config,
            _services
        );
    }
}

public enum RenderApi
{
    None,
    OpenGL
}