using System.Management;
namespace ReisProduction.Wincore.System;
public static class ServiceManager
{
    /// <summary>
    /// Existses the specified Windows service by name.
    /// </summary>
    public static bool Exists(string serviceName)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT * FROM Win32_Service WHERE Name = '{serviceName}'");
            return searcher.Get().Count > 0;
        }
        catch { return false; }
    }
    /// <summary>
    /// Starts the specified Windows service by name.
    /// </summary>
    public static bool Start(string serviceName)
    {
        try
        {
            using ManagementObject service = new($"Win32_Service.Name='{serviceName}'");
            return (uint)service.InvokeMethod("StartService", null) is 0;
        }
        catch { return false; }
    }
    /// <summary>
    /// Stops the specified Windows service by name.
    /// </summary>
    public static bool Stop(string serviceName)
    {
        try
        {
            using ManagementObject service = new($"Win32_Service.Name='{serviceName}'");
            return (uint)service.InvokeMethod("StopService", null) is 0;
        }
        catch { return false; }
    }
    /// <summary>
    /// Restarts the specified Windows service by name.
    /// </summary>
    public static bool Restart(string serviceName, int sleepMilisecond = 789)
    {
        try
        {
            if (!Stop(serviceName)) return false;
            Thread.Sleep(sleepMilisecond);
            return Start(serviceName);
        }
        catch { return false; }
    }
    /// <summary>
    /// Gets the description of the specified Windows service by name.
    /// </summary>
    public static string GetDescription(string serviceName)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT Description FROM Win32_Service WHERE Name = '{serviceName}'");
            foreach (var svc in searcher.Get().Cast<ManagementObject>())
            {
                var desc = svc["Description"]?.ToString();
                if (string.IsNullOrEmpty(desc)) continue;
                return desc.EndsWith('.') ? desc : desc + ".";
            }
            return "Description not found.";
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Gets the current status of the specified Windows service by name.
    /// </summary>
    public static string GetStatus(string serviceName)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT State FROM Win32_Service WHERE Name = '{serviceName}'");
            foreach (var svc in searcher.Get().Cast<ManagementObject>())
                return svc["State"]?.ToString() ?? "Unknown";
            return "Service not found.";
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Gets the start mode of the specified Windows service by name.
    /// </summary>
    public static string GetStartMode(string serviceName)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT StartMode FROM Win32_Service WHERE Name = '{serviceName}'");
            foreach (var svc in searcher.Get().Cast<ManagementObject>())
                return svc["StartMode"]?.ToString() ?? "Unknown";
            return "Service not found.";
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Gets the executable path of the specified Windows service by name.
    /// </summary>
    public static string GetPath(string serviceName)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT PathName FROM Win32_Service WHERE Name = '{serviceName}'");
            foreach (var svc in searcher.Get().Cast<ManagementObject>())
                return svc["PathName"]?.ToString() ?? string.Empty;
            return string.Empty;
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Tries to check if the specified Windows service exists by name.
    /// </summary>
    public static bool TryExists(string serviceName, out bool exists)
    {
        try
        {
            using ManagementObjectSearcher searcher = new($"SELECT * FROM Win32_Service WHERE Name = '{serviceName}'");
            exists = searcher.Get().Count > 0;
            return true;
        }
        catch { exists = false; return false; }
    }
    /// <summary>
    /// Tries to start the specified Windows service by name.
    /// </summary>
    public static bool TryStart(string serviceName, out bool started)
    {
        try
        {
            using ManagementObject service = new($"Win32_Service.Name='{serviceName}'");
            started = (uint)service.InvokeMethod("StartService", null) is 0;
            return true;
        }
        catch { started = false; return false; }
    }
    /// <summary>
    /// Tries to stop the specified Windows service by name.
    /// </summary>
    public static bool TryStop(string serviceName, out bool stopped)
    {
        try
        {
            using ManagementObject service = new($"Win32_Service.Name='{serviceName}'");
            stopped = (uint)service.InvokeMethod("StopService", null) is 0;
            return true;
        }
        catch { stopped = false; return false; }
    }
    /// <summary>
    /// Tries to restart the specified Windows service by name.
    /// </summary>
    public static bool TryRestart(string serviceName, out bool restarted, int sleepMilisecond = 789)
    {
        try
        {
            if (!Stop(serviceName)) { restarted = false; return true; }
            Thread.Sleep(sleepMilisecond);
            restarted = Start(serviceName);
            return true;
        }
        catch { restarted = false; return false; }
    }
    /// <summary>
    /// Tries to get the description of the specified Windows service by name.
    /// </summary>
    public static bool TryGetDescription(string serviceName, out string desc)
    {
        try { desc = GetDescription(serviceName); return true; }
        catch { desc = string.Empty; return false; }
    }
    /// <summary>
    /// Tries to get the current status of the specified Windows service by name.
    /// </summary>
    public static bool TryGetStatus(string serviceName, out string status)
    {
        try { status = GetStatus(serviceName); return true; }
        catch { status = string.Empty; return false; }
    }
    /// <summary>
    /// Tries to get the start mode of the specified Windows service by name.
    /// </summary>
    public static bool TryGetStartMode(string serviceName, out string mode)
    {
        try { mode = GetStartMode(serviceName); return true; }
        catch { mode = string.Empty; return false; }
    }
    /// <summary>
    /// Tries to get the executable path of the specified Windows service by name.
    /// </summary>
    public static bool TryGetPath(string serviceName, out string path)
    {
        try { path = GetPath(serviceName); return true; }
        catch { path = string.Empty; return false; }
    }
}