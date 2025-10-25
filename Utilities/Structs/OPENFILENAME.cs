using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct OPENFILENAME(int lStructSize, nint hwndOwner, nint hInstance,
    string lpstrFilter, string lpstrCustomFilter, int nMaxCustFilter,
    int nFilterIndex, string lpstrFile, int nMaxFile,
    string lpstrFileTitle, int nMaxFileTitle, string lpstrInitialDir,
    string lpstrTitle, uint Flags, short nFileOffset, short nFileExtension,
    string lpstrDefExt, nint lCustData, nint lpfnHook, string lpTemplateName,
    nint pvReserved, int dwReserved, int flagsEx)
{
    public int
        lStructSize = lStructSize,
        nMaxCustFilter = nMaxCustFilter,
        nFilterIndex = nFilterIndex,
        nMaxFile = nMaxFile,
        nMaxFileTitle = nMaxFileTitle,
        dwReserved = dwReserved,
        flagsEx = flagsEx;
    public uint
        Flags = Flags;
    public short
        nFileOffset = nFileOffset,
        nFileExtension = nFileExtension;
    public nint
        hwndOwner = hwndOwner,
        hInstance = hInstance,
        lCustData = lCustData,
        lpfnHook = lpfnHook,
        pvReserved = pvReserved;
    public string
        lpstrFilter = lpstrFilter,
        lpstrCustomFilter = lpstrCustomFilter,
        lpstrFile = lpstrFile,
        lpstrFileTitle = lpstrFileTitle,
        lpstrInitialDir = lpstrInitialDir,
        lpstrTitle = lpstrTitle,
        lpstrDefExt = lpstrDefExt,
        lpTemplateName = lpTemplateName;
}