using SharpStone.Core;

namespace HelloWorld.Layers;

internal class MainLayer : ILayer<MainLayer>
{
    public string Name => "Sandbox Main Layer";

    public static MainLayer Create()
    {
        return new MainLayer();
    }
}
