using SharpStone.Core;
using SharpStone.Layers;
using SharpStone.Resources;
using SharpStone.Window;

namespace SharpStone.Setup;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder AddLayer<TLayer>(this IApplicationBuilder builder) 
        where TLayer : ILayer<TLayer>
        => builder.AddLayer(TLayer.Create());
}

internal class ApplicationBuilder : IApplicationBuilder
{
    private IWindow _window = new SDL2Window();
    private ILayerStack _layerStack = new LayerStack();
    private IResourceManager _resources = new ResourceManager();

    public IApplicationBuilder AddLayer(ILayer layer)
    {
        _layerStack.PushLayer(layer);
        return this;
    }

    public IApplicationBuilder AddLayer<TLayer>() where TLayer : ILayer<TLayer>
    {
        _layerStack.PushLayer(TLayer.Create());
        return this;
    }

    public Application Build()
    {
        return new Application(
            _window, 
            _layerStack, 
            _resources);
    }
}
