namespace SharpStone.Core;
public record struct ShaderSource(string VertexShaderSource, string FragmentShaderSource) : IResource<ShaderSource>
{
    private static class ShaderType
    {
        public const int NONE = -1;
        public const int Vertex = 0;
        public const int Fragment = 1;
    }

    public static string Directory => "Shaders";

    public static string Extension => "shader";

    public static ShaderSource FromStream(Stream stream)
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
