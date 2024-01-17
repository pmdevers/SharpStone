namespace SharpStone.Core;

public interface IApplicationBuilder
{
    IApplicationBuilder AddService(IService service);
    IApplicationBuilder AddLayer(ILayer layer);
    IApplicationBuilder Config<T>(T config);
    Application Build();
}
