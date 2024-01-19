using SharpStone.Maths;
using SharpStone.Rendering;

namespace SharpStone.Gui;

public abstract class UIElement
{
    private IVertexArray _vertexArray;
    public Color BackgroundColor { get; set; } = Color.White;


    public void Update()
    {
        
    }
}

public class UIContainer
{
}