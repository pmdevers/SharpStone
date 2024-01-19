namespace SharpStone.Rendering;

public interface IFrameBuffer : IRenderObject
{
    void Resize(uint width, uint height);
    int ReadPixel(uint attachmentIndex, int x, int y);
    void ClearAttachment(uint attachmentIndex, int value);

    uint GetColorAttachmentRendererId(uint index = 0);

    FramebufferSpecification GetSpecification();
}
