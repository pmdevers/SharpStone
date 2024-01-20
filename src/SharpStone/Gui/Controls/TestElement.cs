using SharpStone.Maths;
using SharpStone.Rendering;
using System.Numerics;
using static SharpStone.Application;

namespace SharpStone.Gui.Controls;
public class TestElement : BaseControl
{
    public TestElement()
    {
        
    }

    public override void Draw()
    {
        Renderer.Renderer2D.DrawQuad(new Vector2(0f, 0f), new Vector2(200f, 200f), Color.FromHEX("352F44"));
    }

    public override void Update()
    {
        
    }

    protected override void Disposing(bool disposing)
    {
        
    }
}
