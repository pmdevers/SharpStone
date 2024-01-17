namespace SharpStone.Renderer;

public interface IUniformBuffer
{
    void SetData<T>(T[] data, int offset = 0);
}
