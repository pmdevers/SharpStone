using SharpStone.Resources;

namespace SharpStone.Core;
public interface IResourceManager
{
    public ShaderProgramSource GetShaderSource(string name);
}
