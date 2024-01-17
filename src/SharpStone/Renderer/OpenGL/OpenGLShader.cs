using SharpStone.Core;
using SharpStone.Platform.OpenGL;

using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;
using SharpStone.Maths;


namespace SharpStone.Renderer.OpenGL;
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
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
        }
        // Gl.Uniform1(location, value);
    }

    public void SetFloat2(string name, Vector2 value)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            //Gl.Uniform2(location, value);
        }
    }

    public void SetFloat3(string name, Vector3 value)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            //Gl.Uniform3(location, value);
        }
    }

    public void SetFloat4(string name, Vector4 value)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            //Gl.Uniform4(location, value);
        }
    }

    public void SetInt(string name, int value)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            //Gl.Uniform1(location, value);
        }
    }

    public void SetIntArray(string name, int[] values)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            //Gl.Uniform1(location, values);
        }
    }

    public unsafe void SetMatrix4(string name, Matrix4 value)
    {
        fixed (char* pName = name)
        {
            int location = glGetUniformLocation(_id, pName);
            // Gl.UniformMat4(location, 1, false, (float*)&value);
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

    public void setIntArray(string name, int[] values)
    {
        throw new NotImplementedException();
    }

    public void setFloat(string name, float value)
    {
        throw new NotImplementedException();
    }
}
