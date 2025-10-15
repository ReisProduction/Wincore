using System.Collections.Concurrent;
namespace ReisProduction.Wincore.Config;
/// <summary>
/// Yaml file manager for reading, writing, and managing Yaml files.
/// </summary>
public static class YamlManager
{
    private static readonly ConcurrentDictionary<string, string> _resources = new();
    /// <summary>
    /// Loads a YAML file from the specified path.
    /// </summary>
    public static async Task<Dictionary<string, string>> LoadAsync(string filePath, bool rejectEx = false)
    {
        _resources.Clear();
        Dictionary<string, string> result = [];
        if (!File.Exists(filePath))
            throw new FileNotFoundException("YAML file not found!", filePath);
        var lines = await File.ReadAllLinesAsync(filePath);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#')) continue;
            var parts = trimmed.Split(':', 2);
            if (parts.Length is 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim();
                result[key] = value;
                _resources[key] = value;
            }
            else if (!rejectEx) throw new InvalidOperationException($"Invalid YAML line: {line}");
        }
        return result;
    }
    /// <summary>
    /// Writes or updates a key-value pair in the YAML resources.
    /// </summary>
    public static void Write(string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        _resources[key] = value;
    }
    /// <summary>
    /// Removes a key-value pair from the YAML resources.
    /// </summary>
    public static bool Remove(string key) => _resources.TryRemove(key, out _);
    /// <summary>
    /// Clears all key-value pairs from the YAML resources.
    /// </summary>
    public static void Clear() => _resources.Clear();
    /// <summary>
    /// Indicates whether any resources have been loaded.
    /// </summary>
    public static bool IsLoaded() => !_resources.IsEmpty;
    /// <summary>
    /// Gets all key-value pairs from the YAML resources.
    /// </summary>
    public static Dictionary<string, string> GetAll() => new(_resources);
    /// <summary>
    /// Gets the value associated with the specified key, or an empty string if the key does not exist.
    /// </summary>
    public static string Get(string key) => _resources.TryGetValue(key, out var value) ? value : string.Empty;
    /// <summary>
    /// Saves the current resources to a YAML file at the specified path.
    /// </summary>
    public static async Task SaveAsync(string filePath, int sbCapacity = 1024)
    {
        StringBuilder sb = new(sbCapacity);
        foreach (var kv in _resources)
            sb.AppendLine($"{kv.Key}: {kv.Value}");
        await File.WriteAllTextAsync(filePath, sb.ToString());
    }
    /// <summary>
    /// Tries to load a YAML file and returns a success flag and error message if applicable.
    /// </summary>
    public static async Task<(bool Success, string ErrorMsg)> TryLoadAsync(string filePath, bool rejectEx)
    {
        try
        {
            _resources.Clear();
            await LoadAsync(filePath, rejectEx);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }
    /// <summary>
    /// Tries to write or update a key-value pair in the YAML resources.
    /// </summary>
    public static bool TryWrite(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key) || value is null) return false;
        _resources[key] = value;
        return true;
    }
    /// <summary>
    /// Tries to get the value associated with the specified key.
    /// </summary>
    public static bool TryGet(string key, out string? value)
    {
        value = null;
        if (string.IsNullOrWhiteSpace(key)) return false;
        return _resources.TryGetValue(key, out value);
    }
    /// <summary>
    /// Tries to save the current resources to a YAML file and returns a success flag and error message if applicable.
    /// </summary>
    public static async Task<(bool Success, string ErrorMsg)> TrySaveAsync(string filePath)
    {
        try
        {
            await SaveAsync(filePath);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }
}