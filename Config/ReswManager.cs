using System.Collections.Concurrent;
using System.Xml.Linq;
namespace ReisProduction.Wincore.Config;
/// <summary>
/// Resw file manager for reading, writing, and managing Resw files.
/// </summary>
public static class ReswManager
{
    private static readonly ConcurrentDictionary<string, string> _resources = [];
    /// <summary>
    /// Loads resources from a .resw file.
    /// </summary>
    public static void Load(string filePath, bool rejectEx = false)
    {
        _resources.Clear();
        if (File.Exists(filePath))
        {
            var xml = XDocument.Load(filePath);
            foreach (var data in xml.Descendants("data"))
            {
                var nameAttr = data.Attribute("name");
                var valueElem = data.Element("value");
                if (nameAttr is null || valueElem is null)
                    if (rejectEx) continue;
                    else throw new FileNotFoundException("Bad .resw file: 'name' or 'value' is missing!");
                string key = nameAttr.Value;
                string value = valueElem.Value;
                _resources[key] = value;
            }
        }
        else
            throw new Exception("Resource file is not found!");
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
    /// Saves the current resources to a .resw file.
    /// </summary>
    public static void Save(string filePath)
    {
        XDocument xml = new(new XElement("root",
            _resources.Select(kv => new XElement("data",
                new XAttribute("name", kv.Key),
                new XElement("value", kv.Value)
            ))
        ));
        xml.Save(filePath);
    }
    /// <summary>
    /// Clears all loaded resources.
    /// </summary>
    public static void Clear() => _resources.Clear();
    /// <summary>
    /// Indicates whether any resources are currently loaded.
    /// </summary>
    public static bool IsLoaded() => !_resources.IsEmpty;
    /// <summary>
    /// Gets the value associated with the specified key, or an empty string if the key does not exist.
    /// </summary>
    public static string Get(this string key) => _resources.TryGetValue(key, out var value) ? value : string.Empty;
    /// <summary>
    /// Tries to load resources from a .resw file, returning success status and error message if any.
    /// </summary>
    public static bool TryLoad(string filePath, bool rejectEx, out string errorMsg)
    {
        errorMsg = string.Empty;
        _resources.Clear();
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Resource file is not found!");
            var xml = XDocument.Load(filePath);
            foreach (var data in xml.Descendants("data"))
            {
                var nameAttr = data.Attribute("name");
                var valueElem = data.Element("value");
                if (nameAttr is null || valueElem is null)
                    if (rejectEx) continue;
                    else throw new InvalidOperationException("Bad .resw file: 'name' or 'value' is missing!");
                _resources[nameAttr.Value] = valueElem.Value;
            }
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
    public static bool TryRemove(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) return false;
        return _resources.TryRemove(key, out _);
    }
    /// <summary>
    /// Tries to save the current resources to a .resw file, returning success status and error message if any.
    /// </summary>
    public static bool TrySave(string filePath, out string errorMsg)
    {
        errorMsg = string.Empty;
        try
        {
            XDocument xml = new(new XElement("root",
                _resources.Select(kv => new XElement("data",
                    new XAttribute("name", kv.Key),
                    new XElement("value", kv.Value)
                ))
            ));
            xml.Save(filePath);
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