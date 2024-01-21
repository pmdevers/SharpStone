using System.Collections;
using SharpStone.Core;

namespace SharpStone.Gui;


public class ControlCollection(BaseControl owner) : IEnumerable<BaseControl>
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
    public ControlContainer(int left, int right, int width, int height)
    {
        Position = new(left, right);
        Size = new(width, height);
        Controls = new ControlCollection(this);
    }
    public ControlCollection Controls { get; }

    public override void Draw()
    {
        foreach (var control in Controls)
        {
            if(control.Visible) { 
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
}
