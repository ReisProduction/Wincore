using System.Globalization;
using System.Management;
using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.System;
/// <summary>
/// System information retrieval utility class.
/// </summary>
public static class SystemInfo
{
    /// <inheritdoc cref="Environment.UserName"/>
    public static string CurrentUser => Environment.UserName;
    /// <inheritdoc cref="Environment.UserDomainName"/>
    public static string DomainName => Environment.UserDomainName;
    /// <inheritdoc cref="Environment.MachineName"/>
    public static string MachineName => Environment.MachineName;
    /// <inheritdoc cref="Environment.ProcessorCount"/>/>
    public static int ProcessId => Environment.ProcessId;
    /// <inheritdoc cref="Environment.ProcessorCount"/>
    public static bool Is64BitProcess => Environment.Is64BitProcess;
    /// <inheritdoc cref="Environment.ProcessorCount"/>
    public static string OSDescription => RuntimeInformation.OSDescription;
    /// <inheritdoc cref="Environment.ProcessorCount"/>
    public static Architecture OSArchitecture => RuntimeInformation.OSArchitecture;
    public static string SystemDirectory => Environment.SystemDirectory;
    /// <inheritdoc cref="RuntimeInformation.FrameworkDescription"/>
    public static string RuntimeDescription => RuntimeInformation.FrameworkDescription;
    /// <inheritdoc cref="CultureInfo.CurrentCulture.TwoLetterISOLanguageName"/>
    public static string TwoLetterISOLanguageName => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    /// <inheritdoc cref="CultureInfo.CurrentCulture"/>
    public static CultureInfo CurrentCulture => CultureInfo.CurrentCulture;
    /// <inheritdoc cref="Environment.ProcessorCount"/>
    public static string CurrentDirectory => Environment.CurrentDirectory;
    /// <inheritdoc cref="Environment.ProcessorCount"/>
    public static string CLRVersion => Environment.Version.ToString();
    /// <summary>
    /// Gets the Windows version number (e.g., 10.0.19045).
    /// </summary>
    public static string GetWinVersion()
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT Version FROM Win32_OperatingSystem");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
                return obj["Version"]?.ToString() ?? "Unknown";
            throw new InvalidOperationException("No results from WMI query.");
        }
        catch { return Environment.OSVersion.Version.ToString(); }
    }
    /// <summary>
    /// Gets the Windows edition (e.g., Home, Pro, Enterprise).
    /// </summary>
    public static string GetWinEdition()
    {
        var edition = Environment.GetEnvironmentVariable("EditionID");
        if (string.IsNullOrEmpty(edition))
            try
            {
                using ManagementObjectSearcher searcher = new("SELECT Caption FROM Win32_OperatingSystem");
                foreach (var obj in searcher.Get().Cast<ManagementObject>())
                    edition = obj["Caption"]?.ToString();
            }
            catch (Exception ex) { edition = $"Error: {ex.Message}"; }
        return edition ?? "Unknown Windows Version";
    }
    /// <summary>
    /// Gets the system architecture (e.g., x64, ARM64).
    /// </summary>
    public static string GetArchitecture()
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT OSArchitecture FROM Win32_OperatingSystem");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
                return obj[nameof(OSArchitecture)]?.ToString() ?? "Unknown";
            throw new InvalidOperationException("No results from WMI query.");
        }
        catch { return RuntimeInformation.OSArchitecture.ToString(); }
    }
    /// <summary>
    /// Gets the processor name (e.g., AMD Ryzen™ 9 9950X3D).
    /// </summary>
    public static string GetProcessorName()
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT Name FROM Win32_Processor");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
                return obj["Name"]?.ToString()?.Trim() ?? "Unknown";
            throw new InvalidOperationException("No results from WMI query.");
        }
        catch { return "Unknown"; }
    }
    /// <summary>
    /// Gets the GPU name (e.g., NVIDIA GeForce RTX 5090).
    /// </summary>
    public static string GetGpuName()
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT Name FROM Win32_VideoController");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
                return obj["Name"]?.ToString() ?? "Unknown";
            throw new InvalidOperationException("No results from WMI query.");
        }
        catch { return "Unknown"; }
    }
    /// <summary>
    /// Gets the total physical memory in a human-readable format.
    /// </summary>
    public static string GetTotalMemory(int precision = 2, string prefix = "", string suffix = "")
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
                if (long.TryParse(obj["TotalVisibleMemorySize"]?.ToString(), out var kb))
                    return (kb * 1024L).FormatDataSize(precision, prefix, suffix);
        }
        catch { }
        return "Unknown";
    }
    /// <summary>
    /// Gets the system boot time as a formatted string (yyyy-MM-dd HH:mm:ss).
    /// </summary>
    public static string GetBootTime()
    {
        try
        {
            using ManagementObjectSearcher searcher = new("SELECT LastBootUpTime FROM Win32_OperatingSystem");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
            {
                var raw = obj["LastBootUpTime"]?.ToString();
                if (!string.IsNullOrWhiteSpace(raw))
                    return ManagementDateTimeConverter.ToDateTime(raw).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        catch { }
        return "Unknown";
    }
}