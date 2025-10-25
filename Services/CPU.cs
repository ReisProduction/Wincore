using System.Management;
using System.Diagnostics;
using static ReisProduction.Wincore.Models.ManagementHelper;

namespace ReisProduction.Wincore.Services;

public static class CPU
{
    public static string GetString(string property) => SelectFrom(property, "Win32_Processor");
    public static int GetInt(string property) => int.TryParse(GetString(property), out var v) ? v : 0;

    public static string Name => GetString("Name");
    public static int Cores => GetInt("NumberOfCores");
    public static int LogicalProcessors => Environment.ProcessorCount;
    public static int MinClockSpeed => GetInt("MinClockSpeed");
    public static int CurrentClockSpeed => GetInt("CurrentClockSpeed");
    public static int MaxClockSpeed => GetInt("MaxClockSpeed");
    public static int CurrentVoltage => GetInt("CurrentVoltage");
    public static int L1Cache => GetInt("L1CacheSize");
    public static int L2Cache => GetInt("L2CacheSize");
    public static int L3Cache => GetInt("L3CacheSize");
    public static string Manufacturer => GetString("Manufacturer");
    public static string Architecture => GetString("Architecture");
    public static int ProcessorCount => Environment.ProcessorCount;
    public static int ProcessId => Environment.ProcessId;
    public static bool Is64Bit => Environment.Is64BitProcess;

    // ---------------- CPU Load ----------------
    private static PerformanceCounter? cpuCounter;
    private static PerformanceCounter CpuCounter
    {
        get
        {
            if (cpuCounter == null)
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                cpuCounter.NextValue(); // İlk okuma 0 dönebilir, ikinci okuma doğru
            }
            return cpuCounter;
        }
    }

    /// <summary>
    /// Returns CPU usage (%) for total system
    /// </summary>
    public static float Load
    {
        get
        {
            
            return CpuCounter.NextValue();
        }
    }
    /// <summary>
    /// Returns CPU temperature in Celsius using WMI (may not be supported on all systems)
    /// </summary>
    public static float Temperature
    {
        get
        {
            try
            {
                using ManagementObjectSearcher searcher = new(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (var obj in searcher.Get().Cast<ManagementObject>())
                    return (float)((Convert.ToDouble(obj["CurrentTemperature"]) / 10.0) - 273.15);
            }
            catch { }
            return 0;
        }
    }
}