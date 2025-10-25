using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct BLUETOOTH_DEVICE_INFO(int dwSize, ulong Address, uint ulClassOfDevice,
    bool fConnected, bool fRemembered, bool fAuthenticated, string szName)
{
    public int dwSize = dwSize;
    public ulong Address = Address;
    public uint ulClassofDevice = ulClassOfDevice;
    [MarshalAs(UnmanagedType.Bool)]
    public bool fConnected = fConnected,
               fRemembered = fRemembered,
            fAuthenticated = fAuthenticated;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 248)]
    public string szName = szName;
}