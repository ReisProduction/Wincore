using System.Collections.Concurrent;
using System.Xml.Linq;
namespace ReisProduction.Wincore.Config;
/// <summary>
/// Resx file manager for reading, writing, and managing Resx files.
/// </summary>
public static class ResxManager
{
    private static readonly ConcurrentDictionary<string, string> _resources = [];
    /// <summary>
    /// Loads resources from a .resx file.
    /// </summary>
    public static void Load(string filePath, bool rejectEx = false)
    {
        _resources.Clear();
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Resource file not found!", filePath);
        var xml = XDocument.Load(filePath);
        foreach (var data in xml.Descendants("data"))
        {
            var nameAttr = data.Attribute("name");
            var valueElem = data.Element("value");
            if (nameAttr is null || valueElem is null)
                if (rejectEx) continue;
                else throw new InvalidOperationException("Bad .resx file: 'name' or 'value' missing!");
            _resources[nameAttr.Value] = valueElem.Value;
        }
    }
    /// <summary>
    /// Writes or updates a resource key-value pair.
    /// </summary>
    public static void Write(string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        _resources[key] = value;
    }
    /// <summary>
    /// Saves the current resources to a .resx file.
    /// </summary>
    public static void Save(string filePath)
    {
        XDocument xml = new(
            new XElement("root",
                _resources.Select(kv => new XElement("data",
                    new XAttribute("name", kv.Key),
                    new XElement("value", kv.Value)
                ))
            )
        );
        xml.Save(filePath);
    }
    /// <summary>
    /// Gets the value for a given resource key, or an empty string if the key does not exist.
    /// </summary>
    public static string Get(this string key) =>
        _resources.TryGetValue(key, out var value) ? value : string.Empty;
    /// <summary>
    /// Clears all loaded resources.
    /// </summary>
    public static void Clear() => _resources.Clear();
    /// <summary>
    /// Indicates whether any resources are currently loaded.
    /// </summary>
    public static bool IsLoaded() => !_resources.IsEmpty;
    /// <summary>
    /// Tries to load resources from a .resx file, returning success status and error message if any.
    /// </summary>
    public static bool TryLoad(string filePath, bool rejectEx, out string errorMsg)
    {
        try
        {
            errorMsg = string.Empty;
            Load(filePath, rejectEx);
            return true;
        }
        catch (Exception ex)
        {
            errorMsg = ex.Message;
            return false;
        }
    }
    /// <summary>
    /// Tries to write or update a resource key-value pair, returning success status.
    /// </summary>
    public static bool TryWrite(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key) || value is null) return false;
        _resources[key] = value;
        return true;
    }
    /// <summary>
    /// Tries to remove a resource by key, returning success status.
    /// </summary>
    public static bool TryRemove(string key) =>
        !string.IsNullOrWhiteSpace(key) && _resources.TryRemove(key, out _);
    /// <summary>
    /// Tries to save the current resources to a .resx file, returning success status and error message if any.
    /// </summary>
    public static bool TrySave(string filePath, out string errorMsg)
    {
        errorMsg = string.Empty;
        try
        {
            Save(filePath);
            return true;
        }
        catch (Exception ex)
        {
            errorMsg = ex.Message;
            return false;
        }
    }
    /// <summary>
    /// Tries to get the value associated with the specified key, returning success status.
    /// </summary>
    public static bool TryGet(string key, out string? value)
    {
        value = null;
        if (string.IsNullOrWhiteSpace(key)) return false;
        return _resources.TryGetValue(key, out value);
    }
}