using SharpStone.Core;
using SharpStone.Platform.OpenGL;

using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;
using System.Text;
using System.Numerics;
using SharpStone.Maths;


namespace SharpStone.Rendering.OpenGL;
internal unsafe class OpenGLShader : IShader
{
    private uint _id;
    public string Name { get; private set; }

    public OpenGLShader(string name, string vertexSrc, string fragmentSrc)
    {
        _id = glCreateProgram();
        Name = name;
        uint vs = CompileShader(ShaderType.VertexShader, vertexSrc);
        uint fs = CompileShader(ShaderType.FragmentShader, fragmentSrc);

        glAttachShader(_id, vs);
        glAttachShader(_id, fs);

        glLinkProgram(_id);
        glValidateProgram(_id);

        glDeleteShader(vs);
        glDeleteShader(fs);

        Name = name;

    }
    ~OpenGLShader()
    {
        Dispose();
    }

    private uint CompileShader(ShaderType shaderType, string source)
    {
        var id = glCreateShader(shaderType);
        glShaderSource(id, source);
        glCompileShader(id);

        int result;
        glGetShaderiv(id, ShaderParameterName.CompileStatus, &result);

        if (result == 0)
        {
            int length;
            glGetShaderiv(id, ShaderParameterName.InfoLogLength, &length);
            var message = glGetShaderInfoLog(id, length);

            Logger.Error<OpenGLShader>(message);
        }

        return id;
    }


    public void Bind()
    {
        glUseProgram(_id);
    }

    public void SetFloat(string name, float value)
    {
        glUniform1f(GetUniformedLocation(name), value);
    }

    public void SetFloat2(string name, Vector2 value)
    {
        glUniform2f(GetUniformedLocation(name), value.X, value.Y);
    }

    public void SetFloat3(string name, Vector3 value)
    {
        glUniform3f(GetUniformedLocation(name), value.X, value.Y, value.Z);
    }


    public void SetFloat4(string name, Vector4 value)
    {
        glUniform4f(GetUniformedLocation(name), value.X, value.Y, value.Z, value.W);
    }

    public void SetInt(string name, int value)
    {
        glUniform1i(GetUniformedLocation(name), value);
    }

    public void SetIntArray(string name, int[] values)
    {
        fixed(int* pValues = values)
        {
            glUniform1iv(GetUniformedLocation(name), values.Length, pValues);
        }
    }

    public unsafe void SetMatrix4(string name, Matrix4x4 value)
    {
        fixed(float* pValues = value.ToFloats().ToArray())
        {
            glUniformMatrix4fv(GetUniformedLocation(name), 1, false, pValues);
        }
    }

    public void Unbind()
    {
        glUseProgram(0);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (_id > 0)
        {
            glDeleteProgram(_id);
            _id = 0;
        }
    }

    private readonly Dictionary<string, int> _locationCache = [];
    private int GetUniformedLocation(string name)
    {
        if(_locationCache.ContainsKey(name)) { 
            return _locationCache[name]; 
        }

        var location = glGetUniformLocation(_id, name);

        if (location == -1)
        {
            int count = 0;
            glGetProgramiv(_id, ProgramPropertyARB.ActiveUniforms, &count);
            Logger.Warning<OpenGLShader>($"Active Uniforms: {count}!");
                
            for (uint i = 0; i < count; i++)
            {
                int bufSize = 0;
                int length;
                int size;
                uint type;
                var sName = new StringBuilder();
                glGetActiveUniform(_id, i, bufSize, &length, &size, &type, sName);
                Logger.Warning<OpenGLShader>($"Uniform #{i} Type: {type} {sName}!");
            }

            Logger.Warning<OpenGLShader>($"Uniform {name} doesn't exist!");
        }
            
        _locationCache[name] = location;
        return location;
    }
}
