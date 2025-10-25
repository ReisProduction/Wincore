using System.Runtime.InteropServices;
using System.Globalization;
using System.Management;
namespace ReisProduction.Wincore.System;
/// <summary>
/// System information retrieval utility class.
/// </summary>
public static class System
{
    /// <inheritdoc cref="Environment.UserName"/>
    public static string CurrentUser => Environment.UserName;
    /// <inheritdoc cref="Environment.UserDomainName"/>
    public static string DomainName => Environment.UserDomainName;
    /// <inheritdoc cref="Environment.MachineName"/>
    public static string MachineName => Environment.MachineName;
    /// <inheritdoc cref="RuntimeInformation.OSDescription"/>
    public static string OSDescription => RuntimeInformation.OSDescription;
    /// <inheritdoc cref="RuntimeInformation.OSArchitecture"/>
    public static Architecture OSArchitecture => RuntimeInformation.OSArchitecture;
    public static string SystemDirectory => Environment.SystemDirectory;
    /// <inheritdoc cref="RuntimeInformation.FrameworkDescription"/>
    public static string RuntimeDescription => RuntimeInformation.FrameworkDescription;
    /// <inheritdoc cref="CultureInfo.CurrentCulture.TwoLetterISOLanguageName"/>
    public static string TwoLetterISOLanguageName => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    /// <inheritdoc cref="CultureInfo.CurrentCulture"/>
    public static CultureInfo CurrentCulture => CultureInfo.CurrentCulture;
    /// <inheritdoc cref="Environment.CurrentDirectory"/>
    public static string CurrentDirectory => Environment.CurrentDirectory;
    /// <inheritdoc cref="Environment.Version"/>
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