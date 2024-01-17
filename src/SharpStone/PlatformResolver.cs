using System.Runtime.InteropServices;

namespace SharpStone;
public class PlatformResolver
{
    public static bool IsWindows() => IsPlatform(OSPlatform.Windows);
    public static bool IsLinux() => IsPlatform(OSPlatform.Linux);
    public static bool IsOsx() => IsPlatform(OSPlatform.OSX);
    public static bool IsFreeBsd() => IsPlatform(OSPlatform.FreeBSD);
    public static bool IsPlatform(OSPlatform osPlatform) => RuntimeInformation.IsOSPlatform(osPlatform);
}
