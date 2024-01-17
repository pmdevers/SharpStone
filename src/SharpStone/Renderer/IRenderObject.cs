namespace SharpStone.Renderer;

public interface IRenderObject : IDisposable
{
    void Bind();
    void Unbind();
}
