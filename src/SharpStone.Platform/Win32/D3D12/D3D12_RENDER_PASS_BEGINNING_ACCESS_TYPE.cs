namespace SharpStone.Platform.Win32.D3D12;

public enum D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE
{
    D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_DISCARD = 0,
    D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_PRESERVE = D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_DISCARD + 1,
    D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_CLEAR = D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_PRESERVE + 1,
    D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_NO_ACCESS = D3D12_RENDER_PASS_BEGINNING_ACCESS_TYPE_CLEAR + 1
}