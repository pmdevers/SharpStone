using System.Collections;
using SharpStone.Graphics;
using SharpStone.Maths;

namespace SharpStone.Gui.Controls;


public class ControlCollection(BaseControl? owner) : IEnumerable<BaseControl>
{
    private List<BaseControl> _controls = new(200);
    public void Add(BaseControl control)
    {
        if (string.IsNullOrEmpty(control.Name))
        {
            control.Name = control.GetType().Name + UserInterface.GetUniqueId();
        }

        control.Parent = owner;

        _controls.Add(control);

        //if(control.Parent is null) 
        control.OnResize();
    }

    public void Clear()
    {
        foreach (var control in _controls)
        {
            control.Dispose();
        }

        _controls.Clear();
    }


    public IEnumerator<BaseControl> GetEnumerator()
        => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _controls.GetEnumerator();
}


public class ControlContainer : BaseControl
{
    public ControlContainer(float left, float right, float width, float height)
    {
        Position = new(left, right);
        Size = new(width, height);
        Controls = new ControlCollection(this);
        RelativeTo = Corner.TopLeft;
        
    }

    public ControlCollection Controls { get; }
    public Color BackgroundColor { get; set; } = UserInterface.Theme.Surface;


    public override void Draw()
    {
       UserInterface.DrawQuad(Position, Size, BackgroundColor);

        foreach (var control in Controls)
        {
            if (control.Visible)
            {
                control.Draw();
            }
        }
    }

    public override void Update()
    {
        foreach (var control in Controls)
        {
            control.Update();
        }
    }

    protected override void Disposing(bool disposing)
    {
        foreach (var control in Controls)
        {
            control.Dispose();
        }
    }

    public override void OnResize()
    {
        base.OnResize();
        foreach (var control in Controls)
        {
            control.OnResize();
        }
    }
}
