using SharpStone.Core;

namespace SharpStone.Renderer;

public interface IGraphicsContext
{
    void Init();
    void SwapBuffers();

    abstract static IGraphicsContext Create(IWindow window);
}
