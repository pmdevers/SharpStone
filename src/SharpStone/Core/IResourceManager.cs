using SharpStone.Renderer;

namespace SharpStone.Core;
public interface IResourceManager
{
    public IShader GetShader(string name);

    public bool Init(Application app);
    public bool Shutdown();
}
