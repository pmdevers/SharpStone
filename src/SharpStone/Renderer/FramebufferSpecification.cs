namespace SharpStone.Renderer;

public struct FramebufferSpecification()
{
    uint Width = 0;
    uint Height = 0;
    FramebufferAttachmentSpecification Attachments;
    uint Samples = 1;
    bool SwapChainTarget = false;
}
