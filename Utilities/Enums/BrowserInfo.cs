namespace ReisProduction.Wincore.Utilities.Enums;
[Flags]
public enum BrowserInfo : uint
{
    ReturnOnlyFileSystemDirs = 0x00000001,   // BIF_RETURNONLYFSDIRS
    NewDialogStyle = 0x00000040,             // BIF_NEWDIALOGSTYLE
    EditBox = 0x00000010,                    // BIF_EDITBOX
    NoNewFolderButton = 0x00000200,          // BIF_NONEWFOLDERBUTTON
    BrowseForComputer = 0x00001000           // BIF_BROWSEFORCOMPUTER
}