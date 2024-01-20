using FluentAssertions;
using SharpStone.Maths;
using System.Numerics;

namespace SharpStone.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
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
}