using SharpStone.Core;

namespace SharpStone.Rendering;

public interface IGraphicsContext
{
    void Init();
    void SwapBuffers();

    abstract static IGraphicsContext Create(IWindow window);
}
