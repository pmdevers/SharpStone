﻿using System.Runtime.InteropServices;

namespace SharpStone;
internal enum OperatingSystem
{
    Windows,
    MacOs,
    Linux
}

internal static class GlobalConfiguration
{
    public static readonly OperatingSystem Os = GetOperatingSystem();
    private static OperatingSystem GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OperatingSystem.Windows;
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OperatingSystem.MacOs;
        }
        return OperatingSystem.Linux;
    }
}
