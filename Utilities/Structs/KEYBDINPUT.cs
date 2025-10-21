using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct KEYBDINPUT(ushort wVk, ushort wScan, int dwFlags, int time, nint dwExtraInfo)
{
    public ushort
        wVk = wVk,
        wScan = wScan;
    public int
        dwFlags = dwFlags,
        time = time;
    public nint dwExtraInfo = dwExtraInfo;
}