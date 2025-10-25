namespace ReisProduction.Wincore.Utilities.Enums;
[Flags]
public enum OpenFileName : uint
{
    Default = Explorer | PathMustExist | FileMustExist,
    ReadOnly = 0x00000001,              // Open file as read-only
    OverwritePrompt = 0x00000002,       // Prompt before overwriting files
    HideReadOnly = 0x00000004,          // Hide read-only checkbox
    NoChangeDir = 0x00000008,           // Never change the current directory
    ShowHelp = 0x00000010,              // Show help button
    EnableHook = 0x00000020,            // Enable hook function
    EnableTemplate = 0x00000040,        // Use custom template for dialog
    EnableTemplateHandle = 0x00000080,  // Use hInstance instead of hTemplate
    NoValidate = 0x00000100,            // Do not validate file names
    AllowMultiSelect = 0x00000200,      // Allow multiple file selection
    ExtensionDifferent = 0x00000400,    // File extension is different from default
    PathMustExist = 0x00000800,         // Path must exist
    FileMustExist = 0x00001000,         // File must exist
    CreatePrompt = 0x00002000,          // Prompt to create file if not found
    ShareAware = 0x00004000,            // Share aware
    NoReadOnlyReturn = 0x00008000,      // Do not return read-only files
    NoTestFileCreate = 0x00010000,      // Do not test file creation
    NoNetworkButton = 0x00020000,       // Hide Network button
    NoLongNames = 0x00040000,           // Use 8.3 short file names only
    Explorer = 0x00080000,              // Use Explorer-style dialog
    NoDereferenceLinks = 0x00100000,    // Do not follow symbolic links
    LongNames = 0x00200000,             // Support long file names
    EnableIncludeNotify = 0x00400000,   // Enable include notify callback
    EnableSizing = 0x00800000,          // Allow dialog resizing
    DontAddToRecent = 0x02000000,       // Don't add file to recent list
    ForceShowHidden = 0x10000000        // Show hidden/system files
}