using SharpStone.Core;
using SharpStone.Gui.Controls;
using static SharpStone.Application;

namespace HelloWorld.Layers;

internal class MainLayer : Layer
{
    public MainLayer() : base("Sandbox Main Layer")
    {
        UI.Add(new TestElement());
    }

    public override void OnUpdate(float v)
    {
    }
}
