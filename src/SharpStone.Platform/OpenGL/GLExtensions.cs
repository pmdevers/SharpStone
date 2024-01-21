using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpStone.Platform.OpenGL;
public static unsafe partial class GL
{

    public static unsafe bool glGetBoolean(GetPName pName)
    {
        bool[] ret = new bool[1];
        fixed(bool* ptr = ret)
        {
            glGetBooleanv(pName, ptr);
        }
        return ret[0];
    }

    public static unsafe uint glGenBuffer()
    {
        uint[] ret = new uint[1];
        fixed (uint* retp = ret)
        {
            glGenBuffers(1, retp);
        }
        return ret[0];
    }

    public static unsafe uint glCreateBuffer()
    {
        uint[] ret = new uint[1];
        fixed (uint* retp = ret)
        {
            glCreateBuffers(1, retp);
        }
        return ret[0];
    }

    public static unsafe uint glGenVertexArray()
    {
        uint[] ret = new uint[1];
        fixed (uint* retp = ret)
        {
            glGenVertexArrays(1, retp);
        }
        return ret[0];
    }

    public static void glShaderSource(uint shader, string source)
    {
        var buffer = Encoding.UTF8.GetBytes(source);
        fixed (byte* p = &buffer[0])
        {
            var sources = new[] { p };
            fixed (byte** s = &sources[0])
            {
                var length = buffer.Length;
                glShaderSource(shader, 1, s, &length);
            }
        }
    }

    public static string glGetShaderInfoLog(uint shader, int bufSize = 1024)
    {
        var buffer = Marshal.AllocHGlobal(bufSize);
        try
        {
            int length;
            var source = (byte*)buffer.ToPointer();
            glGetShaderInfoLog(shader, bufSize, &length, source);
            return PtrToStringUtf8(buffer, length);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private static string PtrToStringUtf8(IntPtr ptr)
    {
        var length = 0;
        while (Marshal.ReadByte(ptr, length) != 0)
            length++;
        var buffer = new byte[length];
        Marshal.Copy(ptr, buffer, 0, length);
        return Encoding.UTF8.GetString(buffer);
    }

    private static string PtrToStringUtf8(IntPtr ptr, int length)
    {
        var buffer = new byte[length];
        Marshal.Copy(ptr, buffer, 0, length);
        return Encoding.UTF8.GetString(buffer);
    }

    public static void glBufferData<T>(BufferTargetARB target, [In, Out] T[] data, BufferUsageARB usage)
            where T : struct
    {
        fixed (void* pData = data)
        {
            var sizeOf = Marshal.SizeOf(typeof(T)) * data.Length;
            glBufferData(target, sizeOf, pData, usage);
        }
    }

    public static void glBufferSubData<T>(BufferTargetARB target, [In, Out] T[] data)
            where T : struct
    {
        fixed (void* pData = data)
        {
            var sizeOf = Marshal.SizeOf(typeof(T)) * data.Length;
            glBufferSubData(target, 0, sizeOf, pData);
        }
    }
}
