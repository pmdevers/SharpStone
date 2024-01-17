namespace SharpStone.Platform.Win32;

public unsafe struct WNDCLASSEXA
{
    public uint CbSize;
    public uint Style;
    public nint LpFnWndProc;
    public int CbClsExtra;
    public int CbWndExtra;
    public HINSTANCE HInstance;
    public nint HIcon;
    public HCURSOR HCursor;
    public nint HbrBackground;
    public char* LpszMenuName;
    public char* LpszClassName;
    public nint HIconSm;
}
