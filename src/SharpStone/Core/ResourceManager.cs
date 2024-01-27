using System.Reflection;

namespace SharpStone.Core;

public interface IResource<TSelf> 
    where TSelf : IResource<TSelf>
{
    abstract static string Directory { get; }
    abstract static string Extension { get; }
    abstract static TSelf FromStream(Stream stream);
}


public static class ResourceManager
{
    private struct Resource(string name, Func<Stream> getStream)
    {
        public string Name = name;
        public Func<Stream> Get = getStream;
        public object? instance;
    }

    private static List<Assembly?> _asssemblies = [
            typeof(ResourceManager).Assembly,
            Assembly.GetEntryAssembly()   
        ];

    private static List<Resource> _resourceNameCache = [];
    
    static ResourceManager()
    {
        ReloadCache();
    }

    public static void AddAssembly(Assembly assembly)
    {
        _asssemblies.Add(assembly);
        ReloadCache();
    }

    public static T GetResource<T>(string name)
        where T : IResource<T>
    {
        var resource = _resourceNameCache.
            FirstOrDefault(x => x.Name
                .EndsWith($"{T.Directory}.{name}.{T.Extension}", 
            StringComparison.InvariantCultureIgnoreCase));

        if(resource.instance == null)
        {
            resource.instance = T.FromStream(resource.Get());
        }

        return (T)resource.instance;
    }

    private static void ReloadCache()
    {
        _resourceNameCache.Clear();
        foreach (var assembly in _asssemblies)
        {
            if (assembly == null)
            {
                continue;
            }
            var names = assembly.GetManifestResourceNames();
            foreach (var name in names)
            {
                _resourceNameCache.Add(new Resource(name, () => assembly.GetManifestResourceStream(name)!));
            }
        }
    }
}