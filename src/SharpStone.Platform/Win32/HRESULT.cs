using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

[StructLayout(LayoutKind.Explicit)]
public struct HRESULT
{
    [FieldOffset(0)]
    public int Value;
    [FieldOffset(0)]
    public uint UnsignedValue;
    public override readonly string ToString() => $"0x{Value:X}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(HRESULT hresult) => hresult.Value;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HRESULT(int hresult) => new() { Value = hresult };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HRESULT(uint hresult) => new() { Value = (int)hresult };
}
