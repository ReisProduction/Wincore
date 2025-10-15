using Microsoft.Win32;
namespace ReisProduction.Wincore.Models;
public static class RegeditManager
{
    /// <summary>
    /// Saves a value to the Windows Registry at the specified key and base key.
    /// </summary>
    public static void SaveRegistry(string key, string value, RegistryKey baseKey = null!)
    {
        baseKey ??= Registry.LocalMachine;
        try { baseKey.SetValue(key, value); }
        catch (Exception ex) { throw new UnauthorizedAccessException($"Could not save registry value: {ex.Message}", ex); }
    }
    /// <summary>
    /// Retrieves a value from the Windows Registry at the specified key and base key.
    /// </summary>
    public static string RetrieveRegistry(string key, RegistryKey? baseKey = null)
    {
        baseKey ??= Registry.LocalMachine;
        try { return baseKey.GetValue(key)?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }
    /// <summary>
    /// Deletes a value or subkey from the Windows Registry at the specified key and base key.
    /// </summary>
    public static void DeleteRegistryValue(string key, string regPath, RegistryKey? baseKey = null)
    {
        baseKey ??= Registry.LocalMachine;
        if (string.IsNullOrWhiteSpace(key))
            baseKey.DeleteSubKeyTree(regPath, false);
        else
            baseKey.OpenSubKey(regPath, true)?.DeleteValue(key, false);
    }
    /// <summary>
    /// Tries to save a value to the Windows Registry at the specified key and base key.
    /// </summary>
    public static bool TrySaveRegistry(string key, string value, string regPath, out string errorMsg, RegistryKey? baseKey = null)
    {
        errorMsg = string.Empty;
        baseKey ??= Registry.LocalMachine;
        try
        {
            using var subKey = baseKey.CreateSubKey(regPath, writable: true)
                ?? throw new InvalidOperationException("Could not create or open the specified registry subkey.");
            subKey.SetValue(key, value);
            return true;
        }
        catch (Exception ex)
        {
            errorMsg = ex.Message;
            return false;
        }
    }
    /// <summary>
    /// Tries to retrieve a value from the Windows Registry at the specified key and base key.
    /// </summary>
    public static bool TryRetrieveRegistry(string key, string regPath, out string value, RegistryKey? baseKey = null)
    {
        value = string.Empty;
        baseKey ??= Registry.LocalMachine;
        try
        {
            using var subKey = baseKey.OpenSubKey(regPath, writable: true)
                ?? throw new InvalidOperationException("Could not open the specified registry subkey.");
            value = subKey.GetValue(key)?.ToString() ?? string.Empty;
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Tries to delete a value or subkey from the Windows Registry at the specified key and base key.
    /// </summary>
    public static bool TryDeleteRegistryValue(string key, string regPath, out string errorMsg, RegistryKey? baseKey = null)
    {
        errorMsg = string.Empty;
        baseKey ??= Registry.LocalMachine;
        try
        {
            if (string.IsNullOrWhiteSpace(key))
                baseKey.DeleteSubKeyTree(regPath, false);
            else
            {
                using var subKey = baseKey.OpenSubKey(regPath, writable: true)
                    ?? throw new InvalidOperationException("Could not open the specified registry subkey.");
                subKey.DeleteValue(key, false);
            }
            return true;
        }
        catch (Exception ex)
        {
            errorMsg = ex.Message;
            return false;
        }
    }
}