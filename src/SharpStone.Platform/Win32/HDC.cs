using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public struct HDC
{
    public nint Value;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nint(HDC hdc) => hdc.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HDC(int hdc) => new() { Value = hdc };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HDC(nint hdc) => new() { Value = hdc };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HDC(nuint hdc) => new() { Value = (nint)hdc };
    public readonly bool IsValid => Value != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() => Value.ToString();
}
