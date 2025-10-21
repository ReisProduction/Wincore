using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct MOUSEINPUT(int dx, int dy, int mouseData, int dwFlags, int time, int dwExtraInfo)
{
    public int
        dx = dx, dy = dy,
        mouseData = mouseData,
        dwFlags = dwFlags,
        time = time;
    public int dwExtraInfo = dwExtraInfo;
}