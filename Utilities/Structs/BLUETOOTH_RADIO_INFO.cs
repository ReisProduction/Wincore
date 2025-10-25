using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct BLUETOOTH_RADIO_INFO(int dwSize, ulong address, string szName,
    uint ulClassOfDevice, ushort lmpSubversion, ushort manufacturer)
{
    public int dwSize = dwSize;
    public ulong address = address;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 248)]
    public string szName = szName;
    public uint ulClassOfDevice = ulClassOfDevice;
    public ushort lmpSubversion = lmpSubversion,
                   manufacturer = manufacturer;
}