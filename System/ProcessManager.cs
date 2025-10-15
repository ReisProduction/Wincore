using System.Security.Principal;
using System.Management;
using System.Security;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Process manager for handling process operations.
/// </summary>
public static class ProcessManager
{
    /// <summary>
    /// Starts a new process with the specified parameters.
    /// </summary>
    public static int Start(string path, string args = "",
        bool waitForExit = false, int waitMilisecond = 3210,
        bool noWindow = false, bool isHidden = false,
        bool useShell = true, bool runAs = false,
        bool redirectStandartOutput = false)
    {
        try
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = path,
                Arguments = args,
                CreateNoWindow = noWindow,
                WindowStyle = isHidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                UseShellExecute = useShell && !redirectStandartOutput,
                RedirectStandardOutput = redirectStandartOutput,
                Verb = runAs ? "runas" : string.Empty
            });
            ArgumentNullException.ThrowIfNull(process);
            if (waitForExit)
                process.WaitForExit(waitMilisecond);
            return process.Id;
        }
        catch { return 0; }
    }
    /// <summary>
    /// Finds the process ID based on the provided ProcessInfo.
    /// </summary>
    public static int FindId(ProcessInfo info)
    {
        var pid = info.Id;
        if (pid is 0)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(info.ProcessName);
            var proc = Process.GetProcessesByName(info.ProcessName).FirstOrDefault()
                       ?? throw new InvalidOperationException($"Process \"{info.ProcessName}\" not found.");
            pid = proc.Id;
        }
        return pid;
    }
    /// <summary>
    /// Finds the process ID based on the provided process name.
    /// </summary>
    public static int FindId(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var p = Process.GetProcessesByName(name);
        return p.Length is 0 ? 0 : p[0].Id;
    }
    /// <summary>
    /// Finds the process name based on the provided process ID.
    /// </summary>
    public static string FindName(int pid)
    {
        try { return Process.GetProcessById(pid).ProcessName; }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Starts time of the process with the specified ID.
    /// </summary>
    public static DateTime StartTime(int pid)
    {
        try { return Process.GetProcessById(pid).StartTime; }
        catch { return DateTime.MinValue; }
    }
    /// <summary>
    /// Process running status check by ID.
    /// </summary>
    public static bool IsRunning(int pid)
    {
        try { return !Process.GetProcessById(pid).HasExited; }
        catch { return false; }
    }
    /// <summary>
    /// Process responding status check by ID.
    /// </summary>
    public static bool IsResponding(int pid)
    {
        try { return Process.GetProcessById(pid).Responding; }
        catch { return false; }
    }
    /// <summary>
    /// Process admin rights check by ID.
    /// </summary>
    public static bool IsAdmin(int pid)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            nint processHandle = OpenProcess(
                PROCESS_QUERY_LIMITED_INFORMATION,
                false,
                process.Id);
            try
            {
                if (processHandle == nint.Zero || !OpenProcessToken(processHandle,
                    TOKEN_QUERY, out nint tokenHandle))
                    return false;
                using WindowsIdentity identity = new(tokenHandle);
                WindowsPrincipal principal = new(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            finally { CloseHandle(processHandle); }
        }
        catch (SecurityException) { return true; }
        catch { return false; }
    }
    /// <summary>
    /// Refreshes the process information for the specified process ID.
    /// </summary>
    public static bool Refresh(int pid)
    {
        try
        {
            Process.GetProcessById(pid).Refresh();
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Command line used to start the process with the specified ID.
    /// </summary>
    public static string CommandLine(int pid)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId={pid}");
            foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                return obj["CommandLine"]?.ToString() ?? "";
        }
        catch { }
        return string.Empty;
    }
    /// <summary>
    /// Description of the process with the specified ID.
    /// </summary>
    public static string Description(int pid)
    {
        try
        {
            var path = ExecutablePath(pid);
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;
            var v = FileVersionInfo.GetVersionInfo(path);
            ArgumentException.ThrowIfNullOrEmpty(v.FileDescription);
            return v.FileDescription;
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Main module file path of the process with the specified ID.
    /// </summary>
    public static string MainModule(int pid)
    {
        try { return Process.GetProcessById(pid).MainModule?.FileName ?? string.Empty; }
        catch { return string.Empty; }
    }
    /// <summary>
    /// File version of the process with the specified ID.
    /// </summary>
    public static string FileVersion(int pid)
    {
        try
        {
            var path = ExecutablePath(pid);
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;
            var v = FileVersionInfo.GetVersionInfo(path);
            ArgumentException.ThrowIfNullOrEmpty(v.FileVersion);
            return v.FileVersion;
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Company name of the process with the specified ID.
    /// </summary>
    public static string Company(int pid)
    {
        try
        {
            var path = ExecutablePath(pid);
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;
            var v = FileVersionInfo.GetVersionInfo(path);
            ArgumentException.ThrowIfNullOrEmpty(v.CompanyName);
            return v.CompanyName;
        }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Session user of the process with the specified ID.
    /// </summary>
    public static string SessionUser(int pid)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId={pid}");
            foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
            {
                var sessionId = Convert.ToInt32(obj["SessionId"]);
                using var searcher2 = new ManagementObjectSearcher($"SELECT * FROM Win32_LogonSession WHERE LogonId={sessionId}");
                foreach (ManagementObject obj2 in searcher2.Get().Cast<ManagementObject>())
                {
                    var userInfo = new string[2];
                    _ = Convert.ToInt32(obj2.InvokeMethod("GetOwner", userInfo));
                    return $"{userInfo[1]}\\{userInfo[0]}";
                }
            }
        }
        catch { }
        return string.Empty;
    }
    /// <summary>
    /// Owner of the process with the specified ID.
    /// </summary>
    public static string Owner(int pid)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId={pid}");
            foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
            {
                var userInfo = new string[2];
                _ = Convert.ToInt32(obj.InvokeMethod("Owner", userInfo));
                return $"{userInfo[1]}\\{userInfo[0]}";
            }
        }
        catch { }
        return string.Empty;
    }
    /// <summary>
    /// Memory usage (in bytes) of the process with the specified ID.
    /// </summary>
    public static long MemoryUsage(int pid)
    {
        try { return Process.GetProcessById(pid).WorkingSet64; }
        catch { return 0; }
    }
    /// <summary>
    /// CPU usage percentage of the process with the specified name.
    /// </summary>
    public static float CpuUsage(string name, int sleepTime = 123)
    {
        try
        {
            using PerformanceCounter pc = new("Process", "% Processor Time", name, true);
            _ = pc.NextValue();
            Thread.Sleep(sleepTime);
            return pc.NextValue() / Environment.ProcessorCount;
        }
        catch { return 0f; }
    }
    /// <summary>
    /// Executable path of the process with the specified ID.
    /// </summary>
    public static string ExecutablePath(int pid, int sbCapacity = 1024, int queryFlag = 0)
    {
        nint handle = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, pid);
        if (handle == nint.Zero)
            throw new InvalidOperationException($"Process with PID {pid} could not be opened.");
        try
        {
            StringBuilder sb = new(sbCapacity);
            var size = sb.Capacity;
            if (!QueryFullProcessImageName(handle, queryFlag, sb, ref size))
                throw new InvalidOperationException($"Unable to query image name for PID {pid}.");
            return sb.ToString(0, size);
        }
        finally { CloseHandle(handle); }
    }
    /// <summary>
    /// Closes the process with the specified ID. If not closed, it can optionally kill the process.
    /// </summary>
    public static bool Close(int pid, bool waitForExit = true, bool killIfNotClose = true, int waitMilisecond = 3210)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            process.Close();
            if (waitForExit)
                process.WaitForExit(waitMilisecond);
            if (!process.HasExited)
            {
                if (killIfNotClose)
                    return Kill(pid, waitForExit, waitMilisecond);
                return false;
            }
            return true;
        }
        catch (Exception ex) { throw new InvalidOperationException($"Unable to kill process with PID {pid}.", ex); }
    }
    /// <summary>
    /// Kills the process with the specified ID.
    /// </summary>
    public static bool Kill(int pid, bool waitForExit = true, int waitMilisecond = 3210)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            process.Kill();
            if (waitForExit)
                process.WaitForExit(waitMilisecond);
            return process.HasExited;
        }
        catch (Exception ex) { throw new InvalidOperationException($"Unable to kill process with PID {pid}.", ex); }
    }
}