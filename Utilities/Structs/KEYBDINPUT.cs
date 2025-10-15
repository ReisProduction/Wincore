using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential)]
public struct KEYBDINPUT
{
    public ushort
        wVk, wScan;
    public int
        dwFlags, time;
    public nint dwExtraInfo;
}