using SharpStone.Events;

namespace SharpStone.Core;
public interface IServiceRegistery : IService
{
    public T GetService<T>() where T : IService;
}
