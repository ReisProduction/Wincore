using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct NativeCredential(uint flags, int type, nint targetName,
    nint comment, global::System.Runtime.InteropServices.ComTypes.FILETIME lastWritten,
    uint credentialBlobSize, nint credentialBlob, uint persist,
    uint attributeCount, nint attributes, nint targetAlias, nint userName)
{
    public uint Flags = flags;
    public int Type = type;
    public nint TargetName = targetName;
    public nint Comment = comment;
    public global::System.Runtime.InteropServices.ComTypes.FILETIME LastWritten = lastWritten;
    public uint CredentialBlobSize = credentialBlobSize;
    public nint CredentialBlob = credentialBlob;
    public uint Persist = persist;
    public uint AttributeCount = attributeCount;
    public nint Attributes = attributes;
    public nint TargetAlias = targetAlias;
    public nint UserName = userName;
}