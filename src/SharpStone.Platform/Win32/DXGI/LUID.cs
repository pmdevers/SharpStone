using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32.DXGI;

[StructLayout(LayoutKind.Sequential)]
public struct LUID
{
    public uint LowPart;
    public int HighPart;
}
