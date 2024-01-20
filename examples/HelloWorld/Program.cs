using HelloWorld.Layers;
using SharpStone;

Application.Create(c =>
{
    c.Name = "Hello World";
})
.PushLayer(new SquareDemoLayer())
.Run();