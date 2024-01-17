using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFO
{
    public int biSize;
    public int biWidth;
    public int biHeight;
    public short biPlanes;
    public short biBitCount;
    public int biCompression;
    public int biSizeImage;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public int biClrUsed;
    public int biClrImportant;

    public void Init()
    {
        biSize = Marshal.SizeOf(this);
    }
}