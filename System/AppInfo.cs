using System.Reflection;
namespace ReisProduction.Wincore.System;
/// <summary>
/// Application information utility class.
/// </summary>
public static class AppInfo
{
    private static readonly FileVersionInfo _fileVersion = FileVersionInfo.GetVersionInfo(_assembly!.Location);
    private static readonly Assembly _assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
    /// <inheritdoc cref="AssemblyName.Name"/>
    public static string Name => _assembly.GetName().Name ?? "Unknown";
    /// <inheritdoc cref="Assembly.FullName"/>
    public static string FullName => _assembly.FullName ?? "Unknown";
    /// <inheritdoc cref="AssemblyName.Version"/>
    public static Version Version => _assembly.GetName().Version ?? new(0, 0, 0, 0);
    /// <inheritdoc cref="FileVersionInfo.FileVersion"/>
    public static string FileVersion => _fileVersion.FileVersion ?? "Unknown";
    /// <inheritdoc cref="FileVersionInfo.ProductVersion"/>
    public static string ProductVersion => _fileVersion.ProductVersion ?? "Unknown";
    /// <inheritdoc cref="FileVersionInfo.ProductName"/>
    public static string ProductName => _fileVersion.ProductName ?? "Unknown";
    /// <inheritdoc cref="FileVersionInfo.CompanyName"/>
    public static string CompanyName => _fileVersion.CompanyName ?? "Unknown";
    /// <inheritdoc cref="FileVersionInfo.LegalCopyright"/>
    public static string LegalCopyright => _fileVersion.LegalCopyright ?? "Unknown";
    /// <inheritdoc cref="FileVersionInfo.Comments"/>
    public static string Description => _fileVersion.Comments ?? _fileVersion.FileDescription ?? "Unknown";
    /// <inheritdoc cref="Assembly.Location"/>
    public static string ExecutablePath => _assembly.Location;
    /// <inheritdoc cref="AppContext.BaseDirectory"/>
    public static string BaseDirectory => AppContext.BaseDirectory;
    private const int linkerTimestampOffset = 8, peHeaderOffset = 60;
    /// <summary>
    /// Build date and time in UTC extracted from the PE header
    /// </summary>
    public static DateTime BuildDateUtc
    {
        get
        {
            try
            {
                var filePath = _assembly.Location;
                var buffer = new byte[2048];
                using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
                _ = stream.Read(buffer, 0, buffer.Length);
                var offset = BitConverter.ToInt32(buffer, peHeaderOffset);
                var secondsSince1970 = BitConverter.ToInt32(buffer, offset + linkerTimestampOffset);
                DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(secondsSince1970);
            }
            catch { return DateTime.MinValue; }
        }
    }
    /// <inheritdoc cref="DateTime.ToLocalTime"/>
    public static DateTime BuildDateLocal => BuildDateUtc.ToLocalTime();
    /// <summary>
    /// Ellapsed time since the application started
    /// </summary>
    public static TimeSpan Uptime => DateTime.Now - Process.GetCurrentProcess().StartTime;
    /// <inheritdoc cref="Process.StartTime"/>
    public static DateTime StartTime => Process.GetCurrentProcess().StartTime;
    /// <inheritdoc cref="AssemblyName.CultureName"/>
    public static string Culture => _assembly.GetName().CultureName ?? "neutral";
}