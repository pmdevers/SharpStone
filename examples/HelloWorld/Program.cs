using HelloWorld.Layers;
using SharpStone;

Application.Create(c =>
{
    c.Name = "Hello World";
})
//.PushLayer(new SquareDemo())
.PushLayer(new MainLayer())
//.PushLayer(new TextureDemo())
.Run();