namespace ReisProduction.Wincore.Services;
/// <summary>
/// Interop services for Windows API and process management.
/// </summary>
public static class Interop
{
    /// <summary>
    /// Show a message box using Windows API. Specify title, text, buttons and icon.
    /// </summary>
    public static DialogResult MessageBoxShow(string title, string text, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information) => _ =
        (DialogResult)MessageBoxW(nint.Zero, text, title, (uint)icon | (uint)buttons);
    /// <summary>
    /// Shutdown or restart the PC immediately or after a delay. Optionally force close applications. Can also schedule or abort scheduled shutdown/restart.
    /// </summary>
    public static void ShutdownOrRestartPC(bool shutDown, int time = 0, bool force = false, bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("shutdown", $"/{(shutDown ? "s" : "r")} /t {time}{(force ? " /f " : "")}", isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Abort an ongoing shutdown or restart.
    /// </summary>
    public static void AbortShutdown(bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("shutdown", "/a", isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Schedule a shutdown or restart using Windows Task Scheduler. Optionally force close applications. Can also abort scheduled shutdown/restart or get time remaining until scheduled shutdown/restart.
    /// </summary>
    public static void ScheduleShutdownOrRestart(bool shutDown, int time = 0, bool force = false, bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("powershell", $"-Command \"{$@"
            Get-ScheduledJob -Name '{ScheduledJobName}' -ErrorAction SilentlyContinue | Unregister-ScheduledJob -Force
            $triggerTime = (Get-Date).AddSeconds({time})
            $actionScript = {{
                param(`$ShutdownAction)
                shutdown /{(shutDown ? "s" : "r")} /t 0{(force ? " /f" : "")}
            }}
            $trigger = New-JobTrigger -Once -At `$triggerTime
            Register-ScheduledJob -Name '{ScheduledJobName}' -ScriptBlock `$actionScript -ArgumentList '{(shutDown ? "shutdown" : "restart")}' -Trigger `$trigger
        ".Replace("\"", "\\\"")}\"",
            isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Abort a scheduled shutdown or restart.
    /// </summary>
    public static void AbortScheduledShutdown(bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("powershell", $"-Command \"{$@"Get-ScheduledJob -Name '{ScheduledJobName}' -ErrorAction SilentlyContinue | Unregister-ScheduledJob -Force"}\"", isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Get the time remaining until a scheduled shutdown or restart. Returns null if no scheduled shutdown/restart is found or an error occurs.
    /// </summary>
    public static TimeSpan? GetScheduledShutdownRemaining(bool isHidden = true, bool noWindow = true, bool runAs = false, int waitTime = 3210)
    {
        try
        {
            var process = Process.GetProcessById(ProcessManager.Start("powershell", $"-Command \"{$@"
                `$job = Get-ScheduledJob -Name '{ScheduledJobName}' -ErrorAction SilentlyContinue
                if (`$job -and `$job.Triggers) {{
                    `$nextRun = `$job.Triggers[0].At
                    `$remaining = `$nextRun - (Get-Date)
                    if (`$remaining -gt [TimeSpan]::Zero) {{
                        return `$remaining.ToString()
                    }}
                }}
                return 'null'
            "}\"", isHidden: isHidden, noWindow: noWindow, runAs: runAs, redirectStandartOutput: true));
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(waitTime);
            if (!string.IsNullOrWhiteSpace(output) &&
                output.Trim() is not "null" &&
                TimeSpan.TryParse(output.Trim(),
                out TimeSpan remaining))
                    return remaining;
            throw new InvalidOperationException("No scheduled shutdown found.");
        }
        catch { return null; }
    }
    /// <summary>
    /// Delay a shutdown or restart using PowerShell's Start-Sleep. Optionally force close applications. Can also stop the delayed shutdown/restart.
    /// </summary>
    public static void DelayedShutdown(bool shutDown, int time = 0, bool force = false, bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("powershell", $"-Command \"{$@"
            Start-Sleep -Seconds {time}
            shutdown /{(shutDown ? "s" : "r")}{(force ? " /f" : "")}
        "}\"", isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Abort a delayed shutdown or restart initiated by DelayedShutdown method.
    /// </summary>
    public static void AbortDelayedShutdown(bool isHidden = true, bool noWindow = true, bool runAs = false) =>
        ProcessManager.Start("powershell", $"-Command \"{@"
            Get-WmiObject Win32_Process | Where-Object { 
                $_.CommandLine -like '*Start-Sleep*' -and $_.CommandLine -like '*shutdown*' 
            } | ForEach-Object { $_.Terminate() }
        "}\"", isHidden: isHidden, noWindow: noWindow, runAs: runAs);
    /// <summary>
    /// Exit the application with an optional log entry and exit code. Log entry is created only if logPath is provided.
    /// </summary>
    public static async Task EnvironmentExit(string logPath = "", int exitCode = 0)
    {
        if (!string.IsNullOrWhiteSpace(logPath))
            await Models.LogManager.CreateLogAsync("Exit Time: " + DateTime.Now.ToString("dd:MM:yyyy"), logPath);
        Environment.Exit(exitCode);
    }
}