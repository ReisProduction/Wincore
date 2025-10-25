using System.Runtime.InteropServices;
using WinRT.Interop;
namespace ReisProduction.Wincore.System;
/// <summary>
/// Native file and folder picker dialogs with Windows API.
/// </summary>
public static class NativePicker
{
    /// <summary>
    /// Shows an open file dialog and returns the selected file path.
    /// </summary>
    public static string ShowOpenFileDialog(object parentWindow,
        string title = "Select File", string initialDir = "",
        string filter = "All Files\0*.*\0", OpenFileName flags = OpenFileName.Default)
    {
        OPENFILENAME ofn = new()
        {
            lStructSize = Marshal.SizeOf<OPENFILENAME>(),
            hwndOwner = parentWindow is not null
                        ? WindowNative.GetWindowHandle(parentWindow)
                        : GetActiveWindow(),
            lpstrFilter = filter,
            lpstrFile = new(new char[256]),
            nMaxFile = 256,
            lpstrFileTitle = new(new char[64]),
            nMaxFileTitle = 64,
            lpstrTitle = title,
            lpstrInitialDir = initialDir,
            Flags = (uint)flags
        };
        return GetOpenFileName(ref ofn) ? ofn.lpstrFile : string.Empty;
    }
    /// <summary>
    /// Shows a folder picker dialog and returns the selected folder path.
    /// </summary>
    public static string ShowFolderPicker(object parentWindow,
        string title = "Select directory",
        BrowserInfo flag = BrowserInfo.NewDialogStyle | BrowserInfo.ReturnOnlyFileSystemDirs)
    {
        BROWSEINFO bi = new()
        {
            hwndOwner = parentWindow is not null
                         ? WindowNative.GetWindowHandle(parentWindow)
                         : GetActiveWindow(),
            lpszTitle = title,
            ulFlags = (uint)flag
        };
        var pidl = SHBrowseForFolder(ref bi);
        string selectedPath = string.Empty;
        if (pidl == nint.Zero) return selectedPath;
        var pathBuffer = new char[260];
        var pathPtr = Marshal.UnsafeAddrOfPinnedArrayElement(pathBuffer, 0);
        if (SHGetPathFromIDList(pidl, pathPtr))
            selectedPath = new(pathBuffer, 0, Array.IndexOf(pathBuffer, '\0'));
        Marshal.FreeCoTaskMem(pidl);
        return selectedPath;
    }
}