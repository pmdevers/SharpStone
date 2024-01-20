using System.Numerics;

namespace SharpStone.Maths;
public static class Matrix4x4Extensions
{
    public static IEnumerable<float> ToFloats(this Matrix4x4 matrix)
    {
        yield return matrix.M11;
        yield return matrix.M12;
        yield return matrix.M13;
        yield return matrix.M14;
        yield return matrix.M21; 
        yield return matrix.M22;
        yield return matrix.M23;
        yield return matrix.M24;
        yield return matrix.M31;
        yield return matrix.M32;
        yield return matrix.M33;
        yield return matrix.M34;
        yield return matrix.M41;
        yield return matrix.M42;
        yield return matrix.M43;
        yield return matrix.M44;
    }
}
