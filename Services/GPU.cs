using static ReisProduction.Wincore.Models.ManagementHelper;
namespace ReisProduction.Wincore.Services;
public static class GPU
{
    public static string GetString(string property) => SelectFrom(property, "Win32_VideoController");
    public static int GetInt(string property) => int.TryParse(GetString(property), out var v) ? v : 0;
    public static string Name => GetString("Name");
    public static string DriverVersion => GetString("DriverVersion");
    public static string VideoProcessor => GetString("VideoProcessor");
    public static double MemoryMB => GetInt("AdapterRAM").ToMegaBytes();
}