using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct KBDLLHOOKSTRUCT
{
    public int
        vkCode,
        scanCode,
        flags,
        time;
    public nint dwExtraInfo;
}