using FluentAssertions;
using SharpStone.Core;
using SharpStone.Graphics;
using SharpStone.Maths;
using SharpStone.Utils;
using System.Numerics;

namespace SharpStone.Tests;

public record struct Shader(string VertexShaderSource, string FragmentShaderSource) : IResource<Shader>
{
    private static class ShaderType
    {
        public const int NONE = -1;
        public const int Vertex = 0;
        public const int Fragment = 1;
    }

    public static string Directory => "Shaders";

    public static string Extension => "shader";

    public static Shader FromStream(Stream stream)
    {
        using var reader = new StreamReader(stream);

        var dict = new string[2];
        var shaderType = ShaderType.NONE;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (line.Contains("#shader"))
            {
                if (line.EndsWith("vertex"))
                {
                    shaderType = ShaderType.Vertex;
                }
                else if (line.EndsWith("fragment"))
                {
                    shaderType = ShaderType.Fragment;
                }
            }
            else
            {
                dict[shaderType] += line + Environment.NewLine;
            }
        }

        return new(dict[ShaderType.Vertex], dict[ShaderType.Fragment]);
    }
}

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void QuadTest()
    {
        Vector4[] _baseQuadPositions = [
            new Vector4(-0.5f, -0.5f, 0.0f, 1.0f),
            new Vector4(0.5f, -0.5f, 0.0f, 1.0f),
            new Vector4(0.5f, 0.5f, 0.0f, 1.0f),
            new Vector4(-0.5f, 0.5f, 0.0f, 1.0f)
        ];

        var result = GenerateQuadVertices(new Vector2(0, 0), new Vector2(3,3));

        Renderer.DrawQuad(new Vector2(0, 0), new Vector2(3, 3), Color.White);

        //Vector4 position1 = Vector4.Transform(new Vector2(-0.5f, -0.5f), scale);
        //Vector4 position2 = Vector4.Transform(new Vector2( 0.5f, -0.5f), scale);
        //Vector4 position3 = Vector4.Transform(new Vector2( 0.5f,  0.5f), scale);
        //Vector4 position4 = Vector4.Transform(new Vector2(-0.5f,  0.5f), scale);




    }

    static Vector4[] GenerateQuadVertices(Vector2 position, Vector2 size)
    {
        // Vertices in local space
        Vector4[] localVertices =
        [
            new Vector4(-0.5f, -0.5f, 0.0f, 1.0f), // Bottom left
            new Vector4( 0.5f, -0.5f, 0.0f, 1.0f), // Bottom right
            new Vector4( 0.5f,  0.5f, 0.0f, 1.0f), // Top right
            new Vector4(-0.5f,  0.5f, 0.0f, 1.0f)  // Top left
        ];

        // Apply position and size to each vertex
        for (int i = 0; i < localVertices.Length; i++)
        {
            localVertices[i].X = localVertices[i].X * size.X + position.X;
            localVertices[i].Y = localVertices[i].Y * size.Y + position.Y;
        }

        return localVertices;
    }

    [Test]
    public void Resources()
    {
        var resource = ResourceManager.GetResource<Shader>("UISolid");




    }

    [Test]
    public void CameraTest()
    {
        int width = 1280;
        int height = 720;


        // Define a 2D vector representing the original position of a point
        Vector3 originalPosition = new(100, 100, 0f);

        // Apply modeling transformations (translate, rotate, scale)
        Matrix4x4 modelMatrix = Matrix4x4.CreateTranslation(0f, 0f, 0f)
                             * Matrix4x4.CreateRotationX(0)
                             * Matrix4x4.CreateScale(1f / width, 1f / height, 1f, new Vector3(0f));

        // Apply the model matrix to the original position
        Vector3 transformedPosition = Vector3.Transform(originalPosition, modelMatrix);

        // Print the result
        Console.WriteLine("Original Position: " + originalPosition);
        Console.WriteLine("Transformed Position: " + transformedPosition);

        var TranslationMatrix = Matrix4x4.CreateOrthographicOffCenter(0, width, 0, height, -1, 1);
        TranslationMatrix = TranslationMatrix * Matrix4x4.CreateScale(1, -1, 1);
        var TranslatedPoint = Vector4.Transform(new Vector4(width, height, 0, 1), TranslationMatrix);

    }

    public void Matrix()
    {

    }


    [Test]
    public void Test2()
    {

        var rgb = Convert.ToInt32("5C5470", 16);
        
        int r = (rgb & 0xff0000) >> 16;
        int g = (rgb & 0xff00) >> 8;
        int b = (rgb & 0xff);
        var rgbd = (uint)(r * 0.299f + g * 0.587f + b * 0.114f) / 256;
        var t = new Color(rgbd);
    }

    [Test]
    public void Test1()
    {
        uint maxQuads = 3;
        uint vertexSize = 6;
        uint indices = vertexSize * maxQuads;

        uint[] _quadIndices = new uint[indices];
        uint offset = 0;
        for (uint i = 0; i < indices; i += vertexSize)
        {
            _quadIndices[i + 0] = 0 + offset;
            _quadIndices[i + 1] = 1 + offset;
            _quadIndices[i + 2] = 2 + offset;
            _quadIndices[i + 3] = 2 + offset;
            _quadIndices[i + 4] = 3 + offset;
            _quadIndices[i + 5] = 0 + offset;
            
            offset += 4;
        }

        uint[] expected = [
            0, 1, 2,
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
           10, 11, 8
        ];

        _quadIndices.Should().BeEquivalentTo( expected, o => o.WithStrictOrdering());

    }

    [Test]
    public void TextureResource()
    {
        ResourceManager.AddAssembly(GetType().Assembly);

        var value = ResourceManager.GetResource<TextureSource>("SharpStone");
        
        
    }
}