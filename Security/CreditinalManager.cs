using System.Runtime.InteropServices;
using System.ComponentModel;
namespace ReisProduction.Wincore.Security;
/// <summary>
/// Credential manager for handling Windows Credential Manager operations.
/// </summary>
public static class CredentialManager
{
    /// <summary>
    /// Saves a credential (key-value pair) to the Windows Credential Manager.
    /// </summary>
    public static void SaveCredential(string value, string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        var bytes = Encoding.Unicode.GetBytes(value);
        nint passwordPtr = nint.Zero, targetPtr = nint.Zero;
        try
        {
            passwordPtr = Marshal.AllocCoTaskMem(bytes.Length);
            Marshal.Copy(bytes, 0, passwordPtr, bytes.Length);
            targetPtr = Marshal.StringToCoTaskMemUni(key);
            NativeCredential cred = new()
            {
                AttributeCount = 0,
                Attributes = nint.Zero,
                Comment = nint.Zero,
                TargetAlias = nint.Zero,
                Type = CRED_TYPE_GENERIC,
                Persist = CRED_PERSIST_LOCAL_MACHINE,
                CredentialBlobSize = (uint)bytes.Length,
                TargetName = targetPtr,
                CredentialBlob = passwordPtr,
                UserName = nint.Zero
            };
            if (!CredWrite(ref cred, 0))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        finally
        {
            if (passwordPtr != nint.Zero) Marshal.FreeCoTaskMem(passwordPtr);
            if (targetPtr != nint.Zero) Marshal.FreeCoTaskMem(targetPtr);
        }
    }
    /// <summary>
    /// Retrieves a credential (value) from the Windows Credential Manager using the specified key.
    /// </summary>
    public static string RetrieveCredential(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        nint credentialPtr = nint.Zero;
        try
        {
            if (!CredRead(key, CRED_TYPE_GENERIC, 0, out credentialPtr)) return string.Empty;
            var credential = Marshal.PtrToStructure<NativeCredential>(credentialPtr);
            return Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2) ?? string.Empty;
        }
        finally
        {
            if (credentialPtr != nint.Zero)
                CredFree(credentialPtr);
        }
    }
    /// <summary>
    /// Deletes a credential from the Windows Credential Manager using the specified target name.
    /// </summary>
    public static bool DeleteCredential(string targetName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(targetName);
        return CredDelete(targetName, CRED_TYPE_GENERIC, 0);
    }
    /// <summary>
    /// Existses the credential in the Windows Credential Manager using the specified target name.
    /// </summary>
    public static bool Exists(string targetName) => CredRead(targetName, CRED_TYPE_GENERIC, 0, out nint ptr) && ptr != nint.Zero;
    /// <summary>
    /// Tries to save a credential (key-value pair) to the Windows Credential Manager.
    /// </summary>
    public static bool TrySaveCredential(string value, string key, out int errorCode)
    {
        try { SaveCredential(value, key); errorCode = 0; return true; }
        catch (Win32Exception ex) { errorCode = ex.NativeErrorCode; return false; }
        catch { errorCode = -1; return false; }
    }
    /// <summary>
    /// Tries to retrieve a credential (value) from the Windows Credential Manager using the specified key.
    /// </summary>
    public static bool TryRetrieveCredential(string key, out string? value)
    {
        try { value = RetrieveCredential(key); return true; }
        catch { value = string.Empty; return false; }
    }
    /// <summary>
    /// Tries to delete a credential from the Windows Credential Manager using the specified target name.
    /// </summary>
    public static bool TryDeleteCredential(string targetName)
    {
        try { return DeleteCredential(targetName); }
        catch { return false; }
    }
}