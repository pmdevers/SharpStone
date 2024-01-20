using SharpStone.Gui;
using SharpStone.Gui.Controls;

namespace SharpStone.Core;
public interface IUserInterface
{
    public void Add(BaseControl control);
    bool Visible { get; set; }
    public bool Init();

    public void Update();

    public bool Shutdown();

}
