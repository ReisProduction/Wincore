namespace ReisProduction.Wincore.Models;
/// <summary>
/// Log manager for creating log files.
/// </summary>
public static class LogManager
{
    /// <summary>
    /// Creates a general log file at the specified log path.
    /// </summary>
    public static async Task CreateLogAsync(string log, string logPath)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(logPath);
            Directory.CreateDirectory(logPath);
            string logFilePath = Path.Combine(logPath, "General.log");
            bool append = File.Exists(logFilePath);
            using StreamWriter writer = new(logFilePath, true);
            if (append)
                await writer.WriteLineAsync("\n\n\nA different time:\n");
            await writer.WriteLineAsync($"Time: {DateTime.Now}\nLog: {log}");
        }
        catch (Exception ex2) { ExceptionManager.ShowError($"Error creating log: {ex2.Message}"); }
    }
    /// <summary>
    /// Creates a log file for the given exception at the specified log path.
    /// </summary>
    public static async Task CreateLogAsync(Exception ex, string logPath)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(logPath);
            Directory.CreateDirectory(logPath);
            string logFilePath = Path.Combine(logPath, $"{ex.GetType().Name}.log");
            bool append = File.Exists(logFilePath);
            await using StreamWriter writer = new(logFilePath, true);
            if (append)
                await writer.WriteLineAsync("\n\n\nA different time:\n");
            await writer.WriteLineAsync($"Time: {DateTime.Now}\nException Message: {ex.Message}");
        }
        catch (Exception ex2) { ExceptionManager.ShowError($"Error creating log: {ex2.Message}"); }
    }
}