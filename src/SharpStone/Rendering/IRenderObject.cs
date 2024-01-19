namespace SharpStone.Rendering;

public interface IRenderObject : IDisposable
{
    void Bind();
    void Unbind();
}
