using SharpStone.Events;

namespace SharpStone.Core;
public interface IService
{
    bool Init(Application app);
    void Update();
    bool Shutdown(Application app);
    void OnEvent(Event e);
}
