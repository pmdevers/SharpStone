using SharpStone.Core;
using SharpStone.Platform.OpenGL;
using System.Runtime.InteropServices;
using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;

namespace SharpStone.Layers;
internal unsafe class DebugLayer : ILayer<DebugLayer>
{
    public string Name => "DebugLayer";

    public static DebugLayer Create()
    {
        return new DebugLayer();
    }

    public bool Init(Application app)
    {
        float[] positions = [
            -0.5f, -0.5f,
             0.0f,  0.5f,
             0.5f, -0.5f,
        ];

        var vba = glGenVertexArray();

        glBindVertexArray(vba);

        var buffer = glGenBuffer();
        glBindBuffer(BufferTargetARB.ArrayBuffer, buffer);
        glBufferData<float>(BufferTargetARB.ArrayBuffer, 6 * sizeof(float), positions, BufferUsageARB.StaticDraw);

        glEnableVertexAttribArray(0);
        glVertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, null);

        var shader = CreatShader(_vertShader, _fragmentShader);
        glUseProgram(shader);

        

        return true;
    }

    public void Update()
    {
        glClearColor(0f, 0f, 0f, 1f);
        glClear((uint)AttribMask.ColorBufferBit);
        
        glDrawArrays(PrimitiveType.Triangles, 0, 3);
    }


    public static uint CompileShader(ShaderType type, string source)
    {
        var id = glCreateShader(type);
        glShaderSource(id, source);
        glCompileShader(id);

        int result;
        glGetShaderiv(id, ShaderParameterName.CompileStatus, &result);

        if(result == 0)
        {
            int length;
            glGetShaderiv(id, ShaderParameterName.InfoLogLength, &length);
            var message = glGetShaderInfoLog(id, length);

            Logger.Error<DebugLayer>(message);
        }

        return id;
    }

    public static uint CreatShader(string vertexShader, string fragmentShader)
    {
        uint program = glCreateProgram();

        var vs = CompileShader(ShaderType.VertexShader, vertexShader);
        var fs = CompileShader(ShaderType.FragmentShader, fragmentShader);

        glAttachShader(program, vs);
        glAttachShader(program, fs);

        glLinkProgram(program);
        glValidateProgram(program);

        glDeleteShader(vs);
        glDeleteShader(fs);

        return program;

    }

    private readonly string _vertShader = @"
#version 330 core

layout(location = 0) in vec4 position;

void main() {
    gl_Position = position;
}
";

    private readonly string _fragmentShader = @"

#version 330 core

layout(location = 0) out vec4 color;

uniform vec4 u_Color;

void main() {
    color = vec4(1.0, 0.0, 0.0, 1.0);
}

";
}
