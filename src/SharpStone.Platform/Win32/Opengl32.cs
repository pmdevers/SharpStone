using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SharpStone.Platform.Win32;
public unsafe partial struct Opengl32
{
    private const string DllName = "opengl32.dll";

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial nint wglGetProcAddress(
        [MarshalAs(UnmanagedType.LPWStr)] string functionName
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HGLRC wglGetCurrentContext();
    
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HDC wglGetCurrentDC();
    
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HGLRC wglCreateContext(HDC hdc);
    
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool wglMakeCurrent(HDC hdc, HGLRC hglrc);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool wglDeleteContext(HGLRC hglrc);
    
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool wglShareLists(HGLRC hglrcDest, HGLRC hglrsSrc);

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.LPTStr)]
    public delegate string wglGetExtensionsStringARBDelegate(HDC dc);
    public static string wglGetExtensionsStringARB(HDC dc)
    {
        var ptr = wglGetProcAddress("wglGetExtensionsStringARB");
        var t = Marshal.GetLastPInvokeError();
        var t2 = Marshal.GetLastPInvokeErrorMessage();

        Debug.Assert(ptr == IntPtr.Zero, "Load Failed!");
        var del = Marshal.GetDelegateForFunctionPointer<wglGetExtensionsStringARBDelegate>(ptr);
        return del(dc);
    }

    
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public delegate bool wglChoosePixelFormatARBDelegate(
        IntPtr dc,
        [In] int[] attribIList,
        [In] float[] attribFList,
        uint maxFormats,
        [Out] int[] pixelFormats,
        out uint numFormats
    );
}
