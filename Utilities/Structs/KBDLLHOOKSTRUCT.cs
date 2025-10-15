using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential)]
public struct KBDLLHOOKSTRUCT
{
    public int
        vkCode,
        scanCode,
        flags,
        time;
    public nint dwExtraInfo;
}