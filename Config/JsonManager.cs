using System.Text.Json;
namespace ReisProduction.Wincore.Config;
/// <summary>
/// Json manager for serializing and deserializing objects to and from JSON format.
/// </summary>
public static class JsonManager
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
    private static readonly JsonSerializerOptions CompactOptions = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true
    };
    /// <summary>
    /// Serialize an object to a JSON string.
    /// </summary>
    public static string Serialize<T>(T obj, bool indented = true) =>
        JsonSerializer.Serialize(obj, indented ? DefaultOptions : CompactOptions);
    /// <summary>
    /// Deserialize a JSON string to an object of type T.
    /// </summary>
    public static T? Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json, DefaultOptions);
    /// <summary>
    /// Serialize an object to a JSON file.
    /// </summary>
    public static void SerializeToFile<T>(T obj, string filePath, bool indented = true) =>
        File.WriteAllText(filePath, Serialize(obj, indented));
    /// <summary>
    /// Deserialize a JSON file to an object of type T.
    /// </summary>
    public static T? DeserializeFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath)) return default;
        return Deserialize<T>(File.ReadAllText(filePath));
    }
    /// <summary>
    /// Serialize an object to a JSON file asynchronously.
    /// </summary>
    public static async Task SerializeToFileAsync<T>(T obj, string filePath, bool indented = true)
    {
        await using var stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, obj, indented ? DefaultOptions : CompactOptions);
    }
    /// <summary>
    /// Deserialize a JSON file to an object of type T asynchronously.
    /// </summary>
    public static async Task<T?> DeserializeFromFileAsync<T>(string filePath)
    {
        if (!File.Exists(filePath)) return default;
        return await JsonSerializer.DeserializeAsync<T>(File.OpenRead(filePath), DefaultOptions);
    }
}