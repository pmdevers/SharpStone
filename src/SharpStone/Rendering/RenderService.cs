using SharpStone.Rendering.OpenGL;

namespace SharpStone.Rendering;

public enum RenderApi
{
    None,
    OpenGL
}

internal class RenderService
{
    internal static IRenderApi Create(RenderApi renderApi)
        => renderApi switch
        {
            RenderApi.OpenGL => new OpenGLRenderer(),
            _ => throw new NotSupportedException("Render not supported!.")
        };
}
