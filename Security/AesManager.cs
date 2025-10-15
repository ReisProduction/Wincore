using System.Security.Cryptography;
namespace ReisProduction.Wincore.Security;
/// <summary>
/// AesManager provides methods for AES encryption and decryption of data, strings, and files.
/// </summary>
public class AesManager
{
    private static readonly Encoding _encoding = Encoding.UTF8;
    /// <summary>
    /// Derives a key of specified size from the given string using SHA256 hashing.
    /// </summary>
    public static byte[] DeriveKey(string key, int size = 32)
    {
        var bytes = SHA256.HashData(_encoding.GetBytes(key));
        return bytes[..size];
    }
    /// <summary>
    /// Encrypts the given data using AES encryption with the provided key.
    /// </summary>
    public static byte[] Encrypt(byte[] data, string key)
    {
        using var aes = Aes.Create();
        aes.Key = DeriveKey(key);
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        using var enc = aes.CreateEncryptor();
        var cipher = enc.TransformFinalBlock(data, 0, data.Length);
        var output = new byte[aes.IV.Length + cipher.Length];
        Buffer.BlockCopy(aes.IV, 0, output, 0, aes.IV.Length);
        Buffer.BlockCopy(cipher, 0, output, aes.IV.Length, cipher.Length);
        return output;
    }
    /// <summary>
    /// Decrypts the given data using AES decryption with the provided key.
    /// </summary>
    public static byte[] Decrypt(byte[] data, string key)
    {
        using var aes = Aes.Create();
        aes.Key = DeriveKey(key);
        aes.IV = data[..16];
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        using var dec = aes.CreateDecryptor();
        return dec.TransformFinalBlock(data, 16, data.Length - 16);
    }
    /// <summary>
    /// Encrypts or decrypts a string using AES encryption and returns the result as a Base64 string.
    /// </summary>
    public static string EncryptString(string text, string key)
        => Convert.ToBase64String(Encrypt(_encoding.GetBytes(text), key));
    /// <summary>
    /// Decrypts a Base64 string using AES decryption and returns the result as a regular string.
    /// </summary>
    public static string DecryptString(string base64, string key)
        => _encoding.GetString(Decrypt(Convert.FromBase64String(base64), key));
    /// <summary>
    /// Encrypts or decrypts a file asynchronously using AES encryption.
    /// </summary>
    public static async Task EncryptFileAsync(string inputPath, string outputPath, string key)
    {
        var data = await File.ReadAllBytesAsync(inputPath);
        var encrypted = Encrypt(data, key);
        await File.WriteAllBytesAsync(outputPath, encrypted);
    }
    /// <summary>
    /// Decrypts a file asynchronously using AES decryption.
    /// </summary>
    public static async Task DecryptFileAsync(string inputPath, string outputPath, string key)
    {
        var data = await File.ReadAllBytesAsync(inputPath);
        var decrypted = Decrypt(data, key);
        await File.WriteAllBytesAsync(outputPath, decrypted);
    }
}