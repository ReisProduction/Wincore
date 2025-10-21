using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct HARDWAREINPUT(int uMsg, short wParamL, short wParamH)
{
    public int uMsg = uMsg;
    public short
        wParamL = wParamL,
        wParamH = wParamH;
}