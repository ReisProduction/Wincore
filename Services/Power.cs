using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.System;
public static class Power
{
    public static Guid GetActiveScheme()
    {
        _ = PowerGetActiveScheme(nint.Zero, out var ptr);
        var guid = Marshal.PtrToStructure<Guid>(ptr);
        Marshal.FreeHGlobal(ptr);
        return guid;
    }
    public static uint SetActiveScheme(Guid guid) => PowerSetActiveScheme(nint.Zero, guid);
    public static bool IsOnAC()
    {
        GetSystemPowerStatus(out var status);
        return status.ACLineStatus is 1;
    }
    public static byte GetBatteryPercent()
    {
        GetSystemPowerStatus(out var status);
        return status.BatteryLifePercent;
    }
    public static uint ApplyPowerPlanAuto(Guid acPlan, Guid batteryPlan)
    {
        var plan = IsOnAC() ? acPlan : batteryPlan;
        return SetActiveScheme(plan);
    }
}