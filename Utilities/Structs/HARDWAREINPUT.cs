using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential)]
public struct HARDWAREINPUT
{
    public int uMsg;
    public short
        wParamL,
        wParamH;
}