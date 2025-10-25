using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct SYSTEM_POWER_STATUS(byte ACLineStatus, byte BatteryFlag, byte BatteryLifePercent,
        byte Reserved1, int BatteryLifeTime, int BatteryFullLifeTime)
{
    public byte
        ACLineStatus = ACLineStatus,
         BatteryFlag = BatteryFlag,
  BatteryLifePercent = BatteryLifePercent,
           Reserved1 = Reserved1;
    public int
        BatteryLifeTime = BatteryLifeTime,
    BatteryFullLifeTime = BatteryFullLifeTime;
}