namespace SharpStone.Rendering;

public interface IUniformBuffer
{
    void SetData<T>(T[] data, int offset = 0);
}
