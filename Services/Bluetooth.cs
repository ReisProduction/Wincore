using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Services;
public static class Bluetooth
{
    public static bool EnableDiscovery(bool enable) => BluetoothEnableDiscovery(nint.Zero, enable) == 0;
    public static bool EnableIncoming(bool enable) => BluetoothEnableIncomingConnections(nint.Zero, enable) == 0;
    public static BLUETOOTH_RADIO_INFO[] GetRadios()
    {
        List<BLUETOOTH_RADIO_INFO> list = [];
        BLUETOOTH_FIND_RADIO_PARAMS param = new() { dwSize = Marshal.SizeOf<BLUETOOTH_FIND_RADIO_PARAMS>() };
        nint radio, findHandle = BluetoothFindFirstRadio(ref param, out radio);
        if (findHandle != nint.Zero)
        {
            do
            {
                BLUETOOTH_RADIO_INFO info = new() { dwSize = Marshal.SizeOf<BLUETOOTH_RADIO_INFO>() };
                if (BluetoothGetRadioInfo(radio, ref info) is 0) list.Add(info);
            }
            while (BluetoothFindNextRadio(findHandle, out radio));
            BluetoothFindRadioClose(findHandle);
        }
        return [.. list];
    }
    public static bool IsDiscoverable(nint hRadio) => BluetoothIsDiscoverable(hRadio);
    public static bool IsConnectable(nint hRadio) => BluetoothIsConnectable(hRadio);
    public static bool AuthenticateDevice(nint hRadio, BLUETOOTH_DEVICE_INFO device, string passkey)
    {
        StringBuilder sb = new(passkey);
        return BluetoothAuthenticateDevice(nint.Zero, hRadio, ref device, sb, (uint)sb.Length) is 0;
    }
    public static bool RemoveDevice(Guid address) => BluetoothRemoveDevice(ref address) is 0;
    public static bool SetServiceState(nint hRadio, BLUETOOTH_DEVICE_INFO device, Guid serviceGuid, bool enable)
        => BluetoothSetServiceState(hRadio, ref device, ref serviceGuid, enable ? 1u : 0u) is 0;
}