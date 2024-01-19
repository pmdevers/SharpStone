using SharpStone.Maths;
using SharpStone.Rendering;

namespace SharpStone.Gui;
public static class RendererExtensions
{
    public static IVertexArray CreateQuad(this IRenderFactory factory, IShader shader)
    {
        float[] positions = [
           -0.5f, -0.5f,
            0.5f, -0.5f,
            0.5f,  0.5f,
           -0.5f,  0.5f
       ];

        uint[] indices = [
            0, 1, 2,
            2, 3, 0
        ];

        var vba = factory.CreateVertexArray();
        var vertexBuffer = factory.CreateVertexBuffer(positions);
        var indexBuffer = factory.CreateIndexBuffer(indices);

        vertexBuffer.Layout.Add("Index", ShaderDataType.Float);

        vba.AddVertexBuffer(vertexBuffer);
        vba.SetIndexBuffer(indexBuffer);

        shader.Bind();

        return vba;
    }


    public static IVertexArray CreateQuad(this IRenderFactory factory, Vector2 location, Vector2 size)
    {
        //float[] vertices = [
        //    location.X, location.Y, 
        //    (location.X + size.X), location.Y,
        //    location.X + size.X, location.Y + size.Y, 
        //    location.X, location.Y + size.Y
        //];

        ////Vector2[] uvs = [
        ////    new(0, 0), 
        ////    new(1, 0), 
        ////    new(1, 1), 
        ////    new(0, 1)
        ////];

        //uint[] indices = [ 
        //    0, 1, 2, 
        //    2, 3, 0 
        //];

        //var va = factory.CreateVertexArray();
        //var vertexBuffer = factory.CreateVertexBuffer(vertices);

        //vertexBuffer.Layout.Add("in_position", ShaderDataType.Float);

        //var ib = factory.CreateIndexBuffer(indices);

        //va.AddVertexBuffer(vertexBuffer);
        //va.SetIndexBuffer(ib);

        //va.Bind();
        Vector3[] positions = [
            new(location.X, location.Y, 0f),
            new(location.X + size.X, location.Y, 0f),
            new(location.X + size.X, location.Y + size.Y, 0f),
            new(location.X, location.Y + size.Y, 0f)
        ];

        uint[] indices = [
            0,
            1,
            2,
            2,
            3,
            0
        ];

        var _vertexArray = factory.CreateVertexArray();
        var vertexBuffer = factory.CreateVertexBuffer(positions);

        vertexBuffer.Layout.Add("in_position", ShaderDataType.Float3);

        var ib = factory.CreateIndexBuffer(indices);

        _vertexArray.AddVertexBuffer(vertexBuffer);
        _vertexArray.SetIndexBuffer(ib);

        _vertexArray.Bind();


        return _vertexArray;
    }
}
