using SharpStone.Core;
using SharpStone.Maths;
using SharpStone.Platform.OpenGL;
using SharpStone.Rendering;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Transactions;
using static SharpStone.Application;

namespace HelloWorld.Layers;
internal class UILayer() : Layer("UI Layer")
{
    private readonly UITEST _ui = new();
    public override void OnAttach()
    {
        _ui.Init();
    }

    public override void OnUpdate(float v)
    {
        _ui.Update();
    }

    public override void OnGuiRender()
    {
        _ui.Draw();
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct QuadVertex(Vector3 position, Vector4 color)
{
    public Vector3 Position = position;
    public Vector4 Color = color;
}


public unsafe class UITEST
{
    public const int MaxQuads = 20000;
    public const int VerticesPerQuad = 4;
    public const int IndicesPerQuad = 6;
    
    public const int MaxVertices = MaxQuads * VerticesPerQuad;
    public const int MaxIndices = MaxQuads * IndicesPerQuad;
    
    private IVertexArray _vertexArray1;
    private IShader _shader;

    private readonly List<QuadVertex> _quads = new(MaxVertices);
    private readonly uint[] _quadIndices = new uint[MaxIndices];

    private readonly Vector4[] _baseQuadPositions = [
        new Vector4(-0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4( 0.5f, -0.5f, 0.0f, 1.0f),
        new Vector4( 0.5f,  0.5f, 0.0f, 1.0f),
        new Vector4(-0.5f,  0.5f, 0.0f, 1.0f)
    ];

    public UITEST()
    {
        uint offset = 0;
        for (uint i = 0; i < MaxIndices; i += IndicesPerQuad)
        {
            _quadIndices[i + 0] = 0 + offset;
            _quadIndices[i + 1] = 1 + offset;
            _quadIndices[i + 2] = 2 + offset;
            _quadIndices[i + 3] = 2 + offset;
            _quadIndices[i + 4] = 3 + offset;
            _quadIndices[i + 5] = 0 + offset;

            offset += VerticesPerQuad;
        }
    }

    

    public void Init()
    {
        //DrawQuad(new Vector2(0f, 0f), new Vector2(200f, 200f), Color.FromHEX("352F44"));
        //DrawQuad(new Vector2(5f, 5f), new Vector2(190f, 190f), Color.White);

        //DrawQuad(new Vector2(200f, 200f), new Vector2(190f, 190f), Color.Green);

        _vertexArray1 = Renderer.Factory.CreateVertexArray();

        var vbo1 = Renderer.Factory.CreateVertexBuffer<QuadVertex>(MaxVertices);

        var ib = Renderer.Factory.CreateIndexBuffer(_quadIndices);

        vbo1.Layout.Add("positions", ShaderDataType.Float3);
        vbo1.Layout.Add("colors", ShaderDataType.Float4);
        
        _vertexArray1.AddVertexBuffer(vbo1);
        _vertexArray1.SetIndexBuffer(ib);
        
        vbo1.SetData([.. _quads]);

        _shader = Renderer.Factory.CreateShader("UISolid");
        _shader.Bind();
    }

    public void Update()
    {

    }
    
    public void Draw()
    {
        Renderer.Commands.SetViewPort(0, 0, Window.Width, Window.Height);
        Renderer.Commands.SetClearColor(Color.CornflowerBlue);
        Renderer.Commands.Clear();

        //var screen = new Vector2(Window.Width, Window.Height);

        //var transformationMatrix = Matrix4.CreateScaling(new Vector3(1f / Window.Width,1f / Window.Height, 0f));
        //var viewMatrix = Matrix4.CreateTranslation(new Vector3(-Window.Width / 2, Window.Width / 2, 0f));
                
        var translationMatrix = Matrix4x4.CreateOrthographicOffCenter(0, Window.Width, Window.Height, 0, -1, 10000);
        var rotationMatrix = Matrix4x4.CreateRotationZ(0f);
        var scaleMatrix = Matrix4x4.CreateScale(new Vector3(1f, 1f, 1f));
        
        var modelMatrix = translationMatrix * rotationMatrix * scaleMatrix;


        var view = Matrix4x4.CreateTranslation(-1, 1, 1);


        //var projectionMatrix = Matrix4.Perspective(1.0f, Window.Width / Window.Height, 0.1f, 100.0f);

        //var mvpMatrix = projectionMatrix * viewMatrix * modelMatrix;

        //var vec = new Vector3(0.5f, 0.5f, 0f);

        //var test = translationMatrix * new Vector3(0.5f, 0.5f, 0f);
        _shader.Bind();
        _shader.SetMatrix4("u_Matrix", modelMatrix);

        Renderer.Commands.DrawIndexed(_vertexArray1);
    }
}

public class UIContainer
{

}

public class UIElement
{
    public string Name { get; set; } = string.Empty;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Size { get; set; } = Vector2.Zero;   
    IShader Shader { get; }
   
}
