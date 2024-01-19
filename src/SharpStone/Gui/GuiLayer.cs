using SharpStone.Core;
using SharpStone.Maths;
using SharpStone.Rendering;
using static SharpStone.Application;

namespace SharpStone.Gui;
internal class GuiLayer : Layer
{
    IVertexArray _quad;
    IShader _shader;
    public GuiLayer() : base("Gui Layer")
    {
        //_quad = Renderer.Factory.CreateQuad(new Vector2(-1f, 0f), new Vector2(0.6f, 0.3f));
        //_shader = Renderer.Factory.CreateShader("Default");
        
        //_quad.Bind();
        //_shader.Bind();
    }

    public override void OnUpdate(float v)
    {
        //_shader.SetFloat4("u_Color", new(1.0f, 0.0f, 0.0f, 1.0f));
        //Renderer.Commands.DrawIndexed(_quad);
    }

    public void Begin()
    {
        Renderer.Renderer2D.BeginScene(null, Matrix4.Identity);
    }

    public override void OnGuiRender()
    {
        Renderer.Renderer2D.DrawQuad(new Vector2(-1f, 0f), new Vector2(0.6f, 0.3f), Color.Green);      

    }

    public void End()
    {
        Renderer.Renderer2D.EndScene();

        Renderer.Renderer2D.VertexArray.Bind();
        Renderer.Commands.DrawIndexed(Renderer.Renderer2D.VertexArray);
    }
}
