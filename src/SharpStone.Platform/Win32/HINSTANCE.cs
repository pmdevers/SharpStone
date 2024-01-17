using System.Runtime.CompilerServices;

namespace SharpStone.Platform.Win32;

public struct HINSTANCE
{
    public nint Handle;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe implicit operator HINSTANCE(nint handle)
        => *(HINSTANCE*)&handle;

    public static HINSTANCE NULL => new() { Handle = 0 };
}
