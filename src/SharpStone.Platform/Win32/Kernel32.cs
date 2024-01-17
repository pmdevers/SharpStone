using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32;

public unsafe partial struct Kernel32
{
    private const string DllName = "kernel32";

    public const uint CS_VREDRAW = 0x0001;
    public const uint CS_HREDRAW = 0x0002;
    public const uint CS_OWNDC = 0x0020;


    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HMODULE GetModuleHandleW(
        char* lpModuleName
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial void* VirtualAlloc(
        void* lpAddress,
        nuint dwSize,
        AllocationType flAllocationType,
        AllocationProtect flProtect
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool VirtualFree(
        void* lpAddress,
        nuint dwSize,
        AllocationType dwFreeType
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial void GetSystemInfo(
        SYSTEM_INFO* lpSystemInfo
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial uint WaitForSingleObject(
        HANDLE hHandle,
        uint dwMilliseconds
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HANDLE CreateEventW(
        SECURITY_ATTRIBUTES* lpEventAttributes,
        int bManualReset,
        int bInitialState,
        char* lpName
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HANDLE CreateEventA(
        SECURITY_ATTRIBUTES* lpEventAttributes,
        int bManualReset,
        int bInitialState,
        byte* lpName
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool CloseHandle(HANDLE handle);

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HANDLE CreateFileW(
        char* lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        SECURITY_ATTRIBUTES* lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        HANDLE hTemplateFile
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ReadFile(
        HANDLE hFile,
        void* lpBuffer,
        uint nNumberOfBytesToRead,
        uint* lpNumberOfBytesRead,
        OVERLAPPED* lpOverlapped
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetFileSizeEx(
        HANDLE hFile,
        LARGE_INTEGER* lpFileSize
    );


    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial HANDLE CreateThread(
        SECURITY_ATTRIBUTES* lpThreadAttributes,
        nuint dwStackSize,
        delegate* unmanaged<void*, int> lpStartAddress,
        void* lpParameter,
        uint dwCreationFlags,
        uint* lpThreadId
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial void Sleep(uint milliseconds);
    
    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial uint GetCurrentThreadId();

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    public static partial uint ResumeThread(
        HANDLE hThread
    );

    [LibraryImport(DllName, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetDllDirectoryW(
        char* lpPathName
    );


    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    public static extern HMODULE LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

    [DllImport("kernel32.dll")]
    public static extern nint GetProcAddress(HMODULE hModule, string procName);
}
