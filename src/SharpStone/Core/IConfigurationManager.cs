namespace SharpStone.Core;
public interface IConfigurationManager
{
    void SetConfig<T>(T value);
    T? GetConfig<T>();
}
