using SharpStone.Maths;
using System.Numerics;

namespace SharpStone.Rendering;
public interface IShader
{
    string Name { get; }

    void Bind();
    void Unbind();

    void SetInt(string name, int value);
    void SetIntArray(string name, int[] values);
    void SetFloat(string name, float value);
    void SetFloat2(string name, Vector2 value);
    void SetFloat3(string name, Vector3 value);
    void SetFloat4(string name, Vector4 value);
    
    void SetMatrix4(string name, Matrix4x4 value);

}
