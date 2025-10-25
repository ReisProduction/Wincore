using static ReisProduction.Wincore.Models.ManagementHelper;
using System.Management;
namespace ReisProduction.Wincore.Services;
public static class Battery
{
    public static string GetString(string property) => SelectFrom(property, "Win32_Battery");
    public enum BatteryStatusEnum : byte
    {
        Unknown = 0,
        Charging = 1,
        Discharging = 2,
        NotCharging = 3,
        Full = 4
    }

    /// <summary>
    /// Return battery charge percentage (0-100)
    /// 
    /// </summary>
    public static int ChargePercentage
    {
        get
        {
            try { return int.Parse(GetString("EstimatedChargeRemaining"))); }
            catch { return -1; }
        }
    }

    /// <summary>
    /// Return battery status enum
    /// </summary>
    public static BatteryStatusEnum Status
    {
        get
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT BatteryStatus FROM Win32_Battery");
                foreach (ManagementObject obj in searcher.Get())
                {
                    int status = Convert.ToInt32(obj["BatteryStatus"]);
                    return status switch
                    {
                        1 => BatteryStatusEnum.Charging,
                        2 => BatteryStatusEnum.Discharging,
                        3 => BatteryStatusEnum.NotCharging,
                        4 => BatteryStatusEnum.Full,
                        _ => BatteryStatusEnum.Unknown
                    };
                }
            }
            catch { }
            return BatteryStatusEnum.Unknown;
        }
    }

    /// <summary>Return true if AC power is connected</summary>
    public static bool IsACConnected
    {
        get
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT PowerOnline FROM Win32_Battery");
                foreach (ManagementObject obj in searcher.Get())
                    return Convert.ToBoolean(obj["PowerOnline"]);
            }
            catch { }
            return false;
        }
    }

    /// <summary>Estimated remaining battery runtime in minutes</summary>
    public static int EstimatedRuntimeMinutes
    {
        get
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT EstimatedRunTime FROM Win32_Battery");
                foreach (ManagementObject obj in searcher.Get())
                {
                    int runtime = Convert.ToInt32(obj["EstimatedRunTime"]);
                    return runtime == 0xFFFFFFFF ? -1 : runtime; // 0xFFFFFFFF = unknown
                }
            }
            catch { }
            return -1;
        }
    }

    /// <summary>
    /// Formatted summary of battery info
    /// </summary>
    public static string Summary =>
        $"Charge: {ChargePercentage}% | Status: {Status} | AC Connected: {IsACConnected} | Remaining: {EstimatedRuntimeMinutes} min";
}
