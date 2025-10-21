using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct BLUETOOTH_FIND_RADIO_PARAMS (int dwSize)
{
    public int dwSize = dwSize;
}