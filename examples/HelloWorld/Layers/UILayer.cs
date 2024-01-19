using SharpStone.Core;
using SharpStone.Maths;
using SharpStone.Rendering;
using System.Runtime.InteropServices;
using static SharpStone.Application;

namespace HelloWorld.Layers;
internal class UILayer() : Layer("UI Layer")
{
    private UI _ui = new();
    public override void OnAttach()
    {
        _ui.Init();
    }

    public override void OnUpdate(float v)
    {
        _ui.Draw();
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct QuadVertex(Vector3 Position, Vector4 Color)
{
    public Vector3 Position { get; set; } = Position;
    public Vector4 Color { get; set; } = Color;
}


public unsafe class UI
{
    IVertexArray _vertexArray1;
    IVertexArray _vertexArray2;
    IShader _shader;

    //uint vba;
    //uint iba;

    public UI()
    {
        
    }

    public  void Init()
    {
        // _vertexArray1 = Renderer.Factory.CreateVertexArray();
        
        //QuadVertex[] _quads = [
        //    new QuadVertex(new Vector3(-0.5f, -0.5f, 0f)),
        //    new QuadVertex(new Vector3(0.5f, -0.5f, 0f)),
        //    new QuadVertex(new Vector3(0.5f, 0.5f, 0f)),
        //    new QuadVertex(new Vector3(-0.5f, 0.5f, 0f)),
        //];

        QuadVertex[] _quads = [
            new QuadVertex(new Vector3(-1f, -1f, 0f), new Vector4(1f, 0f, 0f, 1f)),
            new QuadVertex(new Vector3(0f, -1f, 0f), new Vector4(1f, 1f, 0f, 1f)),
            new QuadVertex(new Vector3(0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f)),
            new QuadVertex(new Vector3(-1f, 0f, 0f), new Vector4(0f, 0f, 0f, 1f)),


            new QuadVertex(new Vector3(-0.5f, -0.5f, 0f), new Vector4(1f, 0f, 0f, 1f)),
            new QuadVertex(new Vector3(0.5f, -0.5f, 0f), new Vector4(1f, 1f, 0f, 1f)),
            new QuadVertex(new Vector3(0.5f, 0.5f, 0f), new Vector4(1f, 1f, 1f, 1f)),
            new QuadVertex(new Vector3(-0.5f, 0.5f, 0f), new Vector4(0f, 0f, 0f, 1f)),

            new QuadVertex(new Vector3(-0.2f, -0.2f, 0f), new Vector4(1f, 0f, 0f, 1f)),
            new QuadVertex(new Vector3(0.2f, -0.2f, 0f), new Vector4(1f, 1f, 0f, 1f)),
            new QuadVertex(new Vector3(0.2f, 0.2f, 0f), new Vector4(1f, 1f, 1f, 1f)),
            new QuadVertex(new Vector3(-0.2f, 0.2f, 0f), new Vector4(0f, 0f, 0f, 1f)),
        ];

        //Vector4[] colors = [
        //    new Vector4(1f, 0f, 0f, 1f),
        //    new Vector4(1f, 1f, 0f, 1f),
        //    new Vector4(1f, 1f, 1f, 1f),
        //    new Vector4(0f, 0f, 0f, 1f),
        //];

        uint[] indices = [
            0, 1, 2,
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8
        ];

      
        _vertexArray1 = Renderer.Factory.CreateVertexArray();

        var vbo1 = Renderer.Factory.CreateVertexBuffer<QuadVertex>(20000);

        var ib = Renderer.Factory.CreateIndexBuffer(indices);

        vbo1.Layout.Add("positions", ShaderDataType.Float3);
        vbo1.Layout.Add("colors", ShaderDataType.Float4);
        
        _vertexArray1.AddVertexBuffer(vbo1);
        _vertexArray1.SetIndexBuffer(ib);
        
        vbo1.SetData(_quads);

        _shader = Renderer.Factory.CreateShader("UISolid");
        _shader.Bind();
    }

    public void Draw()
    {
        Renderer.Commands.SetViewPort(0, 0, Window.Width, Window.Height);
        Renderer.Commands.SetClearColor(Color.CornflowerBlue);
        Renderer.Commands.Clear();

        //_shader.SetFloat4("u_Color", new(0.5f, 0.0f, 0.0f, 1.0f));

        //Renderer.Commands.DrawIndexed(_vertexArray2);
        //Renderer.Commands.DrawIndexed(_vertexArray1);
        _shader.Bind();
        Renderer.Commands.DrawIndexed(_vertexArray1);
        //glBindVertexArray(vba);
        //int count = 6;
        //glDrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, null);
    }
}
