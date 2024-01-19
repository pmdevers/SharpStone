namespace SharpStone.Rendering;

public interface IVertexArray : IRenderObject
{
    void AddVertexBuffer(IVertexBuffer vertextBuffer);
    IVertexBuffer[] GetVertextBuffers();
    void SetIndexBuffer(IIndexBuffer indexBuffer);
    IIndexBuffer? GetIndexBuffer();
}
