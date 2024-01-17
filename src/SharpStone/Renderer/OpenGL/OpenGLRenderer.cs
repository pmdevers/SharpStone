using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Maths;
using SharpStone.Platform.OpenGL;
using SharpStone.Window;
using System.Runtime.InteropServices;
using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;

namespace SharpStone.Renderer.OpenGL;
internal unsafe class OpenGLRenderer : IRenderApi
{
    public void Clear()
    {
        glClear((uint)(AttribMask.ColorBufferBit | AttribMask.DepthBufferBit));
    }

    public IGraphicsContext CreateGrapicsContext(IWindow window)
    {
        throw new NotImplementedException();
    }

    public IShader CreateShader(string name)
    {
        throw new NotImplementedException();
    }

    public IShader CreateShader(string name, string vertexSrc, string fragmentSrc)
    {
        return new OpenGLShader(name, vertexSrc, fragmentSrc);
    }

    public ITexture2D CreateTexture(uint width, uint heigth)
    {
        throw new NotImplementedException();
    }

    public ITexture2D CreateTexture(string path)
    {
        throw new NotImplementedException();
    }

    public IUniformBuffer CreateUniformBuffer()
    {
        throw new NotImplementedException();
    }

    public IVertexArray CreateVertexArray()
        => new OpenGLVertexArray();

    public IVertextBuffer CreateVertexBuffer<T>(T[] data, int size)
        where T : struct
        => new OpenGLVertexBuffer<T>(data, size);

    public void DrawIndexed(IVertexArray vertexArray, int? indexCount = null)
    {
        vertexArray.Bind();
        int count = indexCount ?? vertexArray.GetIndexBuffer().Count;
        glDrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, null);
    }

    public void DrawLines(IVertexArray vertexArray, int vertexCount)
    {
        vertexArray.Bind();
        glDrawArrays(PrimitiveType.Lines, 0, vertexCount);
    }

    public void DrawArrays(IVertexArray vertexArray, int vertexCount)
    {
        vertexArray.Bind();
        glDrawArrays(PrimitiveType.Triangles, 0, vertexCount);
    }

    public bool Init(Application app)
    {
        #if DEBUG
        glEnable(EnableCap.DebugOutput);
        glEnable(EnableCap.DebugOutputSynchronous);

        var pCallback = Marshal.GetFunctionPointerForDelegate(DebugCallback);
        glDebugMessageCallback(pCallback, nint.Zero);

        #endif
        glEnable(EnableCap.DebugOutput);
        glEnable(EnableCap.DebugOutputSynchronous);

        return true;
    }

    public void OnEvent(Event e)
    {
        
    }

    public void SetClearColor(Color color)
    {
        glClearColor(color.R, color.G, color.B, color.A);
    }

    public void SetLineWidth(int width)
    {
        glLineWidth(width);
    }

    public void SetViewPort(int x, int y, int width, int height)
    {
        glViewport(x, y, width, height);
    }

    public bool Shutdown(Application app) => true;

    public void Update()
    {
    }

    private static void DebugCallback(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, nint message, void* userParam)
    {
        var logLevel = severity switch
        {
            DebugSeverity.DebugSeverityNotification => LogLevel.Info,
            DebugSeverity.DebugSeverityLow => LogLevel.Debug,
            DebugSeverity.DebugSeverityMedium => LogLevel.Warning,
            DebugSeverity.DebugSeverityHigh => LogLevel.Error,
            _ => LogLevel.Fatal,
        };

        var msg = Marshal.PtrToStringAnsi(message, length);

        Logger.Log<OpenGLRenderer>(logLevel, $"{source} {type} - {new string(msg)}");
    }

    public IIndexBuffer CreateIndexBuffer(uint[] indices, int size)
        => new OpenGLIndexBuffer(indices, size);
}
