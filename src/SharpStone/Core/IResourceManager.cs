using SharpStone.Renderer;

namespace SharpStone.Core;
public interface IResourceManager : IService
{
    public IShader GetShader(string name);
}
