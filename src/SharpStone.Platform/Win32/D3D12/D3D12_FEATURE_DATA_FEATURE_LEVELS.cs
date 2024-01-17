using SharpStone.Platform.Win32.D3D;
using System.Runtime.InteropServices;

namespace SharpStone.Platform.Win32.D3D12;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct D3D12_FEATURE_DATA_FEATURE_LEVELS
{
    public uint NumFeatureLevels;
    public D3D_FEATURE_LEVEL* pFeatureLevelsRequested;
    public D3D_FEATURE_LEVEL MaxSupportedFeatureLevel;
}
