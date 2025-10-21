using System.Security.Cryptography;
namespace ReisProduction.Wincore.Security;
/// <summary>
/// SHA-256 hash manager for computing and verifying hashes.
/// </summary>
public static class SHAManager
{
    /// <summary>
    /// Turns a hexadecimal string representation of the given byte array.
    /// </summary>
    public static string ToHex(byte[] bytes)
    {
        StringBuilder sb = new(bytes.Length * 2);
        foreach (var b in bytes) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
    /// <summary>
    /// Computes the SHA-256 hash of the given input string or byte array, optionally using a salt.
    /// </summary>
    public static string Compute(string input, string? salt = null) => ToHex(SHA256.HashData(Encoding.UTF8.GetBytes(salt is null ? input : salt + input)));
    /// <summary>
    /// Computes the SHA-256 hash of the given byte array, optionally using a salt.
    /// </summary>
    public static string Compute(byte[] data, string? salt = null)
    {
        if (salt is not null)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var combined = new byte[saltBytes.Length + data.Length];
            Buffer.BlockCopy(saltBytes, 0, combined, 0, saltBytes.Length);
            Buffer.BlockCopy(data, 0, combined, saltBytes.Length, data.Length);
            data = combined;
        }
        return ToHex(SHA256.HashData(data));
    }
    /// <summary>
    /// Computes the SHA-256 hash of a file's contents asynchronously.
    /// </summary>
    public static async Task<string> ComputeFileAsync(string filePath)
    {
        await using var stream = File.OpenRead(filePath);
        using var sha = SHA256.Create();
        var hash = await sha.ComputeHashAsync(stream);
        return ToHex(hash);
    }
    /// <summary>
    /// Verifies if the SHA-256 hash of the given input string or byte array matches the expected hash, optionally using a salt.
    /// </summary>
    public static bool Verify(string input, string expectedHash, string? salt = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        => string.Equals(Compute(input, salt), expectedHash, comparison);
    /// <summary>
    /// Verifies if the SHA-256 hash of the given byte array matches the expected hash, optionally using a salt.
    /// </summary>
    public static bool Verify(byte[] data, string expectedHash, string? salt = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        => string.Equals(Compute(data, salt), expectedHash, comparison);
    /// <summary>
    /// Verifies if the SHA-256 hash of a file's contents matches the expected hash asynchronously.
    /// </summary>
    public static async Task<bool> VerifyFileAsync(string filePath, string expectedHash, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        => string.Equals(await ComputeFileAsync(filePath), expectedHash, comparison);
}