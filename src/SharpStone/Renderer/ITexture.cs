namespace SharpStone.Renderer;

public interface ITexture
{
    uint Width { get; }
    uint Height { get; }
    uint RendererId { get; }
    string Path { get; }
    void SetData<T>(T data);
    void Bind(uint slot = 0);
    bool IsLoaded { get; }
}
