using System.Security.Cryptography;
namespace ReisProduction.Wincore.Security;
/// <summary>
/// Hash manager for securely hashing and verifying passwords and files using PBKDF2.
/// </summary>
public static class HashManager
{
    /// <summary>
    /// Iterations for the PBKDF2 algorithm. Higher values increase security but also increase computation time.
    /// </summary>
    public static int Iterations { get; set; } = 100_000;
    /// <summary>
    /// Salt size in bytes. A larger salt increases security by making precomputed attacks more difficult.
    /// </summary>
    public static int SaltSize { get; set; } = 16;
    /// <summary>
    /// Key size in bytes. Common sizes are 16 (128 bits), 32 (256 bits), etc.
    /// </summary>
    public static int KeySize { get; set; } = 32;
    public static int HashByteSize => SaltSize + KeySize;
    public static int HashStringSize => Convert.ToBase64String(new byte[HashByteSize]).Length;
    /// <inheritdoc cref="string.GetHashCode(StringComparison)"/>
    public static int GetHashCode(this string value,
        StringComparison comparison) => value.GetHashCode(comparison);
    /// <summary>
    /// Gets a hash code from the given byte array.
    /// </summary>
    public static int GetHashCode(params byte[] bytes)
    {
        if (bytes.Length != HashByteSize)
            throw new ArgumentException($"Invalid hash byte array size. Expected {HashByteSize} bytes.");
        int hash = 17;
        foreach (var b in bytes)
            hash = hash * 31 + b;
        return hash;
    }
    /// <summary>
    /// Gets a hash code from the given string values.
    /// </summary>
    public static int GetHashCode(params string[] values)
    {
        using MemoryStream ms = new();
        foreach (var str in values)
        {
            if (str is null) continue;
            var bytes = Encoding.UTF8.GetBytes(str);
            ms.Write(bytes, 0, bytes.Length);
        }
        return GetHashCode(ms.ToArray());
    }
    /// <summary>
    /// Generates a random salt.
    /// </summary>
    public static byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(SaltSize);
    /// <summary>
    /// Generates a random key.
    /// </summary>
    public static byte[] GenerateKey() => RandomNumberGenerator.GetBytes(KeySize);
    /// <summary>
    /// Hashes a password using PBKDF2 with a random salt.
    /// </summary>
    public static string Hash(this string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        var result = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(key, 0, result, SaltSize, KeySize);
        return Convert.ToBase64String(result);
    }
    /// <summary>
    /// Verifies a password against a given hash.
    /// </summary>
    public static bool Verify(this string password, string hashed)
    {
        var data = Convert.FromBase64String(hashed);
        var salt = data[..SaltSize];
        var key = data[SaltSize..];
        var testKey = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        return CryptographicOperations.FixedTimeEquals(key, testKey);
    }
    /// <summary>
    /// Hashes the contents of a file using PBKDF2 with a random salt.
    /// </summary>
    public static async Task<string> HashFileAsync(string filePath)
    {
        var bytes = await File.ReadAllBytesAsync(filePath);
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(bytes, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        var result = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(key, 0, result, SaltSize, KeySize);
        return Convert.ToBase64String(result);
    }
    /// <summary>
    /// Verifies the contents of a file against a given hash.
    /// </summary>
    public static async Task<bool> VerifyFileAsync(string filePath, string hashed)
    {
        var data = Convert.FromBase64String(hashed);
        var salt = data[..SaltSize];
        var key = data[SaltSize..];
        var bytes = await File.ReadAllBytesAsync(filePath);
        var testKey = Rfc2898DeriveBytes.Pbkdf2(bytes, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        return CryptographicOperations.FixedTimeEquals(key, testKey);
    }
}