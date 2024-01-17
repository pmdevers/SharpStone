using System.Runtime.CompilerServices;

namespace SharpStone.Platform.Win32;

public struct HMODULE
{
    public nint Handle;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe implicit operator HMODULE(nint handle)
       => *(HMODULE*)&handle;

    public static HMODULE NULL => new() { Handle = 0 };
}
