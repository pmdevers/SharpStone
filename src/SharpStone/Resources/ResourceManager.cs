using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Renderer;

namespace SharpStone.Resources;
internal class ResourceManager : IResourceManager
{
    public IShader GetShader(string name)
    {
        throw new NotImplementedException();
    }

    public bool Init(Application app) => true;
    public void OnEvent(Event e) { }
    public bool Shutdown(Application app) => true;
    public void Update() { }
}
