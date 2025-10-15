namespace ReisProduction.Wincore.Utilities.Structs;
public struct NativeCredential
{
    public uint Flags;
    public int Type;
    public IntPtr TargetName;
    public IntPtr Comment;
    public global::System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
    public uint CredentialBlobSize;
    public IntPtr CredentialBlob;
    public uint Persist;
    public uint AttributeCount;
    public IntPtr Attributes;
    public IntPtr TargetAlias;
    public IntPtr UserName;
}