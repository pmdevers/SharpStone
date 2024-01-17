using SharpStone.Core;
using SharpStone.Renderer;

namespace SharpStone.Resources;
internal class ResourceManager : IResourceManager
{
    public IShader GetShader(string name)
    {
        throw new NotImplementedException();
    }

    public bool Init(Application app) => true;

    public bool Shutdown() => true;
}
