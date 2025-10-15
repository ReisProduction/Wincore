using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
    public int
        dx, dy,
        mouseData,
        dwFlags,
        time;
    public nint dwExtraInfo;
}