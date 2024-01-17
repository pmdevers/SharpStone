using SharpStone.Core;
using SharpStone.Events;

using static SharpStone.Logging;

namespace SharpStone.Services;
internal class ServiceRegistery() : IServiceRegistery
{
    private readonly List<IService> _services = [];

    public void AddService(IService service) 
        => _services.Add(service);
    
    public T GetService<T>() where T : IService, new()
        => _services.OfType<T>().FirstOrDefault() ?? new T();

    public bool Init(Application app) 
    {
        foreach (var service in _services)
        {
            Logger.Assert<ServiceRegistery>(service.Init(app), $"Initialization to failed {service.GetType().Name}.");
        }

        return true;
    }

    public void OnEvent(Event e)
    {
        foreach (var item in _services)
        {
            item.OnEvent(e);
        }
    }

    public bool Shutdown(Application app) 
    {
        foreach(var service in _services)
        {
            Logger.Assert<ServiceRegistery>(service.Shutdown(app), $"Failed to Shutdown {service.GetType().Name}.");
        }

        return true;
    }

    public void Update()
    {
        foreach (var item in _services)
        {
            item.Update();
        }
    }
}
