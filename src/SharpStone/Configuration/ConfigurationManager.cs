using SharpStone.Core;

namespace SharpStone.Configuration;
internal class ConfigurationManager : IConfigurationManager
{
    private readonly Dictionary<string, object> _configs = [];

    public T? GetConfig<T>()
    {
        var key = GetKey<T>();
        return _configs.TryGetValue(key, out var value)
            ? (T)value : default;
    }

    public void SetConfig<T>(T value)
    {
        var v = value ?? default;
        var key = GetKey<T>();
        _configs.Add(key, v!);
    }

    private static string GetKey<T>() => typeof(T).Name;
}
