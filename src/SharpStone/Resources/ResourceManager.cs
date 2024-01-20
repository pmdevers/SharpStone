using SharpStone.Core;
using SharpStone.Events;
using System.Reflection;
using System.Xml.Linq;

namespace SharpStone.Resources;
public static class ShaderType
{
    public const int NONE = -1;
    public const int Vertex = 0;
    public const int Fragment = 1;
}

internal class ResourceManager(Assembly assembly) : IResourceManager
{
    public ShaderProgramSource GetShaderSource(string name)
    {
        var stream = GetResource(assembly, ResourceType.Shaders, name);
        using var reader = new StreamReader(stream);

        var dict = new string[2];
        var shaderType = ShaderType.NONE;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (line.Contains("#shader"))
            {
                if (line.EndsWith("vertex"))
                {
                    shaderType = ShaderType.Vertex;
                }
                else if (line.EndsWith("fragment"))
                {
                    shaderType = ShaderType.Fragment;
                }
            }
            else
            {
                dict[shaderType] += line + Environment.NewLine;
            }
        }

        return new(dict[ShaderType.Vertex], dict[ShaderType.Fragment]);
    }

    public bool Init(Application app) => true;
    public void OnEvent(Event e) { }
    public bool Shutdown(Application app) => true;
    public void Update() { }

    private readonly Dictionary<ResourceType, string> extensions = new()
    {
        { ResourceType.Shaders, "shader" },
    };

    private Stream GetResource(Assembly assembly, ResourceType type, string name)
    {
        try
        {
            return GetResourceStream(assembly, type, name);
        }
        catch (Exception)
        {
            return GetResourceStream(typeof(Application).Assembly, type, name);
        }
    }

    private Stream GetResourceStream(Assembly assembly, ResourceType type, string name)
    {
        var names = assembly.GetManifestResourceNames();
        var file = names.FirstOrDefault(x => x.EndsWith($"{Enum.GetName(type)}.{name}.{extensions[type]}", StringComparison.InvariantCultureIgnoreCase));
        return assembly.GetManifestResourceStream(file);
    }
}

public enum ResourceType
{
    Shaders,
}


public record struct ShaderProgramSource(string VertexShaderSource, string FragmentShaderSource);