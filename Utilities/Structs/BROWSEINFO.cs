using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct BROWSEINFO(
    nint hwndOwner, nint pidlRoot, string pszDisplayName, string lpszTitle,
    uint ulFlags, nint lpfn, nint lParam, int iImage)
{
    public nint
        hwndOwner = hwndOwner,
        pidlRoot = pidlRoot,
        lpfn = lpfn,
        lParam = lParam;
    public string
        pszDisplayName = pszDisplayName,
        lpszTitle = lpszTitle;
    public uint
        ulFlags = ulFlags;
    public int
        iImage = iImage;
}