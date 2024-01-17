using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;
public unsafe partial struct GDI32
{
    private const string DllName = "gdi32";

    /// PFD_FLAGS
    public const uint PFD_DOUBLEBUFFER = 0x00000001;
    public const uint PFD_STEREO = 0x00000002;
    public const uint PFD_DRAW_TO_WINDOW = 0x00000004;
    public const uint PFD_DRAW_TO_BITMAP = 0x00000008;
    public const uint PFD_SUPPORT_GDI = 0x00000010;
    public const uint PFD_SUPPORT_OPENGL = 0x00000020;
    public const uint PFD_GENERIC_FORMAT = 0x00000040;
    public const uint PFD_NEED_PALETTE = 0x00000080;
    public const uint PFD_NEED_SYSTEM_PALETTE = 0x00000100;
    public const uint PFD_SWAP_EXCHANGE = 0x00000200;
    public const uint PFD_SWAP_COPY = 0x00000400;
    public const uint PFD_SWAP_LAYER_BUFFERS = 0x00000800;
    public const uint PFD_GENERIC_ACCELERATED = 0x00001000;
    public const uint PFD_SUPPORT_DIRECTDRAW = 0x00002000;
    public const uint PFD_DIRECT3D_ACCELERATED = 0x00004000;
    public const uint PFD_SUPPORT_COMPOSITION = 0x00008000;
    public const uint PFD_DEPTH_DONTCARE = 0x20000000;
    public const uint PFD_DOUBLEBUFFER_DONTCARE = 0x40000000;
    public const uint PFD_STEREO_DONTCARE = 0x80000000;

    /// PFD_LAYER_TYPES
    public const byte PFD_MAIN_PLANE = 0;
    public const byte PFD_OVERLAY_PLANE = 1;
    public const byte PFD_UNDERLAY_PLANE = 255;

    /// PFD_PIXEL_TYPE
    public const byte PFD_TYPE_RGBA = 0;
    public const byte PFD_TYPE_COLORINDEX = 1;

    //	Unmanaged functions from the Win32 graphics library.
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial int ChoosePixelFormat(
        HDC hDC, 
        PIXELFORMATDESCRIPTOR ppfd
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DescribePixelFormat(
        HDC hDC,
        int iPixelFormat,
        uint nBytes,
        PIXELFORMATDESCRIPTOR* ppfd
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetPixelFormat(
        HDC hDC, 
        int iPixelFormat, 
        PIXELFORMATDESCRIPTOR* ppfd);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial IntPtr GetStockObject(uint fnObject);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial int SwapBuffers(IntPtr hDC);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool BitBlt(IntPtr hDC, int x, int y, int width,
        int height, IntPtr hDCSource, int sourceX, int sourceY, uint type);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi,
       uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DeleteObject(IntPtr hObject);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial IntPtr CreateCompatibleDC(IntPtr hDC);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DeleteDC(IntPtr hDC);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial IntPtr CreateFont(int nHeight, int nWidth, int nEscapement,
       int nOrientation, uint fnWeight, uint fdwItalic, uint fdwUnderline, uint
       fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint
       fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, char* lpszFace);
}


