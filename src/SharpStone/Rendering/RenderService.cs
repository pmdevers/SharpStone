using SharpStone.Rendering.OpenGL;
using System.Net.Http.Headers;

namespace SharpStone.Rendering;

public enum RenderApi
{
    None,
    OpenGL
}

internal class RenderService
{
    internal static IRenderApi Create(RenderApi renderApi)
    {
        var r = renderApi switch
        {
            RenderApi.OpenGL => new OpenGLRenderer(),
            _ => throw new NotSupportedException("Render not supported!.")
        };

        return r;
    }
}
