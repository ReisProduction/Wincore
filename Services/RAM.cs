using static ReisProduction.Wincore.Models.ManagementHelper;
namespace ReisProduction.Wincore.Services;
public static class RAM
{
    static string GetString(string property, string table = "Win32_OperatingSystem") => SelectFrom(property, table);
    public static long Total => long.TryParse(GetString("TotalVisibleMemorySize"), out var kb) ? kb : 0;
    public static long Free => long.TryParse(GetString("FreePhysicalMemory"), out var kb) ? kb : 0;
    public static long Used => Total - Free;
    public static double UsagePercent => Total > 0 ? (double)Used / Total * 100d : 0d;
    public static long TotalVirtual => long.TryParse(GetString("TotalVirtualMemorySize"), out var kb) ? kb : 0;
    public static long FreeVirtual => long.TryParse(GetString("FreeVirtualMemory"), out var kb) ? kb : 0;
    public static long UsedVirtual => TotalVirtual - FreeVirtual;
    public static double VirtualUsagePercent => TotalVirtual > 0 ? (double)UsedVirtual / TotalVirtual * 100d : 0d;
    public static double CombinedUsagePercent
    {
        get
        {
            var totalAll = Total + TotalVirtual;
            var usedAll = Used + UsedVirtual;
            return totalAll > 0 ? usedAll / (double)totalAll * 100d : 0d;
        }
    }
    public static string FormatPhysical() => $"{Math.Round(UsagePercent, 2)}% ({Used / 1024d / 1024d:0.00} GB / {Total / 1024d / 1024d:0.00} GB)";
    public static string FormatVirtual() => $"{Math.Round(VirtualUsagePercent, 2)}% ({UsedVirtual / 1024d / 1024d:0.00} GB / {TotalVirtual / 1024d / 1024d:0.00} GB)";
}