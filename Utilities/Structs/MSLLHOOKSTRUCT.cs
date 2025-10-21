using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct MSLLHOOKSTRUCT
{
    public POINT pt;
    public uint
        mouseData,
        flags,
        time,
        dwExtraInfo;
}