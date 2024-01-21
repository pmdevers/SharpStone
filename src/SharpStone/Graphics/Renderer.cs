using SharpStone.Core;
using SharpStone.Maths;
using SharpStone.Platform.OpenGL;
using System.Runtime.InteropServices;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Graphics;

public static unsafe class RenderCommand
{
    public static void Init()
    {
        glEnable(EnableCap.DebugOutput);
        glEnable(EnableCap.DebugOutputSynchronous);

        var pCallback = Marshal.GetFunctionPointerForDelegate(DebugCallback);
        glDebugMessageCallback(pCallback, nint.Zero);
    }

    public static void SetViewPort(int x, int y, int width, int height)
    {
        glViewport(x, y, width, height);
    }

    public static void SetClearColor(Color color)
    {
        glClearColor(color.R, color.G, color.B, color.A);
    }

    public static void Clear()
    {
        glClear((uint)(AttribMask.ColorBufferBit | AttribMask.DepthBufferBit));
    }

    public static void DrawIndexed(VertexArray vertexArray, int? indexCount = null)
    {
        vertexArray.Bind();
        int count = indexCount ?? vertexArray.GetIndexBuffer().Count;
        glDrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, null);
    }

    public static void DrawArrays(VertexArray vertexArray, int vertexCount)
    {
        vertexArray.Bind();
        glDrawArrays(PrimitiveType.Triangles, 0, vertexCount);
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

        Logger.Log<Renderer>(logLevel, $"{source} {type} - {new string(msg)}");
    }

}
