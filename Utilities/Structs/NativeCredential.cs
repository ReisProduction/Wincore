using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct NativeCredential(uint flags, int type, nint targetName,
    nint comment, FILETIME lastWritten,
    uint credentialBlobSize, nint credentialBlob, uint persist,
    uint attributeCount, nint attributes, nint targetAlias, nint userName)
{
    public uint
                 Flags = flags,
    CredentialBlobSize = credentialBlobSize,
               Persist = persist,
        AttributeCount = attributeCount;
    public int Type = type;
    public nint
        TargetName = targetName,
           Comment = comment,
    CredentialBlob = credentialBlob,
        Attributes = attributes,
       TargetAlias = targetAlias,
          UserName = userName;
    public FILETIME LastWritten = lastWritten;
}