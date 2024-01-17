namespace SharpStone.Renderer;
public interface IShader
{
    string Name { get; }

    void Bind();
    void Unbind();

    void SetInt(string name, int value);
    void setIntArray(string name, int[] values);
    void setFloat(string name, float value);

}
