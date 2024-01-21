using SharpStone.Graphics;
using SharpStone.Maths;

namespace SharpStone.Gui;
public class TestElement : BaseControl
{
    public TestElement()
    {

    }
    public override void Draw()
    {
        Renderer.DrawQuad(Position, Size, Color.White);
    }

    public override void Update()
    {

    }

    protected override void Disposing(bool disposing)
    {

    }
}
