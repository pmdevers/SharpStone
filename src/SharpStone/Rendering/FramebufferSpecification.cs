namespace SharpStone.Rendering;

public struct FramebufferSpecification()
{
    readonly uint Width = 0;
    readonly uint Height = 0;
    readonly FramebufferAttachmentSpecification Attachments;
    readonly uint Samples = 1;
    readonly bool SwapChainTarget = false;
}
