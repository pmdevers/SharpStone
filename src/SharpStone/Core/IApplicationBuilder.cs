namespace SharpStone.Core;

public interface IApplicationBuilder
{
    IApplicationBuilder AddLayer(ILayer layer);
    Application Build();
}
