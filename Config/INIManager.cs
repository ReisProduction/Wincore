namespace ReisProduction.Wincore.Config;
/// <summary>
/// INI file manager for reading, writing, and managing INI files.
/// </summary>
public static class INIManager
{
    /// <summary>
    /// If key exists and has a non-whitespace value, returns true; otherwise, false.
    /// </summary>
    public static bool KeyExists(string section, string key, string iniPath) =>
        !string.IsNullOrWhiteSpace(Read(section, key, iniPath));
    /// <summary>
    /// If section exists (has at least one key), returns true; otherwise, false.
    /// </summary>
    public static bool SectionExists(string section, string iniPath) =>
        ReadAllKeys(section, iniPath).Count > 0;
    /// <summary>
    /// Tries to determine if a key exists and has a non-whitespace value.
    /// </summary>
    public static bool TryKeyExists(string section, string key, string iniPath) =>
        TryRead(section, key, iniPath, out var value) && !string.IsNullOrWhiteSpace(value);
    /// <summary>
    /// Tries to determine if a section exists (has at least one key).
    /// </summary>
    public static bool TrySectionExists(string section, string iniPath) =>
        TryReadAllKeys(section, iniPath, out var keys) && keys.Count > 0;
    /// <summary>
    /// Reads a value from the INI file. If the key does not exist, returns an empty string.
    /// </summary>
    public static string Read(string section, string key, string iniPath)
    {
        StringBuilder retVal = new(1024);
        int len = GetPrivateProfileString(section, key, key, retVal, retVal.Capacity, iniPath);
        return len > 0 ? retVal.ToString(0, len) : string.Empty;
    }
    /// <summary>
    /// Writes a value to the INI file. If the key is null, deletes the entire section.
    /// </summary>
    public static void Write(string section, string? key, string? value, string iniPath) => WritePrivateProfileString(section, key, value, iniPath);
    /// <summary>
    /// Deletes a key or an entire section from the INI file.
    /// </summary>
    public static void DeleteKey(string section, string key, string iniPath) => Write(section, key, null, iniPath);
    /// <summary>
    /// Deletes an entire section from the INI file.
    /// </summary>
    public static void DeleteSection(string section, string iniPath) => Write(section, null, null, iniPath);
    /// <summary>
    /// Reads all keys from a section in the INI file. If the section does not exist, returns an empty list.
    /// </summary>
    public static List<string> ReadAllKeys(string section, string iniPath)
    {
        var inside = false;
        List<string> keys = [];
        var lines = File.ReadLines(iniPath);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith('[') && trimmed.EndsWith(']'))
            {
                inside = trimmed[1..^1].Equals(section, StringComparison.OrdinalIgnoreCase);
                continue;
            }
            if (inside && trimmed.Contains('='))
                keys.Add(trimmed[..trimmed.IndexOf('=')].Trim());
        }
        return keys;
    }
    /// <summary>
    /// Tries to read a value from the INI file. If successful, returns true and outputs the value; otherwise, false.
    /// </summary>
    public static bool TryRead(string section, string key, string iniPath, out string value)
    {
        value = string.Empty;
        try
        {
            if (!File.Exists(iniPath)) return false;
            StringBuilder retVal = new(1024);
            int len = GetPrivateProfileString(section, key, "", retVal, retVal.Capacity, iniPath);
            value = len > 0 ? retVal.ToString(0, len) : string.Empty;
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Tries to write a value to the INI file. If successful, returns true; otherwise, false.
    /// </summary>
    public static bool TryWrite(string section, string? key, string? value, string iniPath)
    {
        try
        {
            WritePrivateProfileString(section, key, value, iniPath);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Tries to delete a key or an entire section from the INI file. If successful, returns true; otherwise, false.
    /// </summary>
    public static bool TryDeleteKey(string section, string key, string iniPath) =>
        TryWrite(section, key, null, iniPath);
    /// <summary>
    /// Tries to delete an entire section from the INI file. If successful, returns true; otherwise, false.
    /// </summary>
    public static bool TryDeleteSection(string section, string iniPath) =>
        TryWrite(section, null, null, iniPath);
    /// <summary>
    /// Tries to read all keys from a section in the INI file. If successful, returns true and outputs the list of keys; otherwise, false.
    /// </summary>
    public static bool TryReadAllKeys(string section, string iniPath, out List<string> keys)
    {
        keys = [];
        try
        {
            if (!File.Exists(iniPath))
                throw new FileNotFoundException("INI file not found.");
            var inside = false;
            foreach (var line in File.ReadLines(iniPath))
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith('[') && trimmed.EndsWith(']'))
                {
                    inside = trimmed[1..^1].Equals(section, StringComparison.OrdinalIgnoreCase);
                    continue;
                }
                if (inside && trimmed.Contains('='))
                {
                    int eqIndex = trimmed.IndexOf('=');
                    if (eqIndex >= 0)
                        keys.Add(trimmed[..eqIndex].Trim());
                }
            }
            return true;
        }
        catch { return false; }
    }
}