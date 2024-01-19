using HelloWorld.Layers;
using SharpStone;

namespace HelloWorld;
public class HelloWorldApp : Application
{
    public HelloWorldApp(ApplicationConfig applicationConfig) 
        : base(applicationConfig)
    {
        //PushLayer(new MainLayer());
        //PushLayer(new SquareDemoLayer());
        PushOverlay(new UILayer());
    }

    public static Application Create(params object[] args)
    {
        var config = new ApplicationConfig();
        config.Name = "Hello World";
        
        return new HelloWorldApp(config);
    }
}
