namespace SharpStone.Platform.Win32.D3D12;

public enum D3D12_FENCE_FLAGS
{
    D3D12_FENCE_FLAG_NONE = 0,
    D3D12_FENCE_FLAG_SHARED = 0x1,
    D3D12_FENCE_FLAG_SHARED_CROSS_ADAPTER = 0x2,
    D3D12_FENCE_FLAG_NON_MONITORED = 0x4
}