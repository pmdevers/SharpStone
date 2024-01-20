using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public struct HWND
{
    public nint Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nint(HWND hwnd) => hwnd.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HWND(int hwnd) => new() { Value = hwnd };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HWND(nint hwnd) => new() { Value = hwnd };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HWND(nuint hwnd) => new() { Value = (nint)hwnd };
    public readonly bool IsValid => Value != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() => Value.ToString();
}
