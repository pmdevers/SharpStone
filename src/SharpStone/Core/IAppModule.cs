namespace SharpStone.Core;

public interface IAppModule
{
    static abstract bool Build(IApplicationBuilder builder);
    static abstract bool Initialize(IApplication application);
    static abstract bool Shutdown(IApplication application);
}
