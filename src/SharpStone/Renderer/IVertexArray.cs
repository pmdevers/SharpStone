namespace SharpStone.Renderer;

public interface IVertexArray : IRenderObject
{
    void AddVertexBuffer(IVertextBuffer vertextBuffer);
    IVertextBuffer[] GetVertextBuffers();
    void SetIndexBuffer(IIndexBuffer indexBuffer);
    IIndexBuffer? GetIndexBuffer();
}
