using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public struct HGLRC
{
    public nint Value;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nint(HGLRC hglrc) => hglrc.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HGLRC(int hglrc) => new() { Value = hglrc };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HGLRC(nint hglrc) => new() { Value = hglrc };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HGLRC(nuint hglrc) => new() { Value = (nint)hglrc };
    public readonly bool IsValid => Value != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() => Value.ToString();
}