using SharpStone.Maths;

namespace SharpStone.Gui.Controls;
public class TestElement : BaseControl
{
    
    public TestElement()
    {

    }
    public override void Draw()
    {
        var alpha = 1f * Math.Clamp(Transparency / 100f, 0, 1);
        var color = new Color(1f, 1f, 1f, alpha);

        

        UserInterface.DrawQuad(CorrectedPosition, Size, color);
        //UserInterface.DrawQuad(Position + new Vector2(3), Size - new Vector2(5), UserInterface.Theme.Primary.Color300);


    }

    public override void Update()
    {

    }

    protected override void Disposing(bool disposing)
    {

    }
}
