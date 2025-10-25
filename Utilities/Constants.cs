namespace ReisProduction.Wincore.Utilities;
public static class Constants
{
    /// <summary>
    /// Public constant values for Windows API functions and settings.
    /// </summary>
    public const int
        // MapVirtualKey translation types
        MAPVK_VK_TO_WK = 0,     // Translates a virtual-key code into a scan code
        MAPVK_WK_TO_VK = 1,     // Translates a scan code into a virtual-key code

        // Input types
        INPUT_MOUSE = 0,            // Mouse input
        INPUT_KEYBOARD = 1,         // Keyboard input
        WH_KEYBOARD_LL = 13,        // Low-level keyboard hook
        WH_MOUSE_LL = 14,           // Low-level mouse hook
        WM_KEYDOWN = 0x0100,        // Key down message
        WM_KEYUP = 0x0101,          // Key up message
        WM_SYSKEYDOWN = 0x0104,     // System key down message
        WM_SYSKEYUP = 0x0105,       // System key up message
        WM_MOUSEMOVE = 0x0200,      // Mouse move message
        WM_MOUSEWHEEL = 0x020A,     // Mouse wheel message
        WM_MOUSEHWHEEL = 0x020E,    // Mouse horizontal wheel message
        WM_LBUTTONDOWN = 0x0201,    // Left button down message
        WM_LBUTTONUP = 0x0202,      // Left button up message
        WM_RBUTTONDOWN = 0x0204,    // Right button down message
        WM_RBUTTONUP = 0x0205,      // Right button up message
        WM_MBUTTONDOWN = 0x0207,    // Middle button down message
        WM_MBUTTONUP = 0x0208,      // Middle button up message
        WM_XBUTTONDOWN = 0x020B,    // X button down message
        WM_XBUTTONUP = 0x020C,      // X button up message
        LLKHF_INJECTED = 0x10,      // Flag indicating the event was injected

        // Mouse wheel constants
        WHEEL_DELTA = -120,       // Default value for one wheel notch movement

        // Credential Types and Persistence
        CRED_TYPE_GENERIC = 1,              // Generic credential type
        CRED_PERSIST_LOCAL_MACHINE = 2,     // Credential persists for all users on the machine

        // Window Long Indexes
        GWL_STYLE = -16,          // Retrieves or sets the window style
        GWL_EXSTYLE = -20,        // Retrieves or sets the extended window style

        // Window Styles
        WS_VISIBLE = 0x10000000,                // Window is initially visible
        WS_OVERLAPPEDWINDOW = 0x00CF0000,       // Common combination of styles for a standard window
        WS_EX_TOOLWINDOW = 0x00000080,          // Creates a tool window (small title bar, not shown in taskbar)
        WS_EX_APPWINDOW = 0x00040000,           // Forces a top-level window to appear on the taskbar
        WS_POPUP = unchecked((int)0x80000000),  // Creates a pop-up window (no border, no title bar)


        // ShowWindow Commands
        SW_HIDE = 0,              // Hides the window
        SW_SHOWNORMAL = 1,        // Activates and displays a window
        SW_SHOWMINIMIZED = 2,     // Shows the window minimized
        SW_MAXIMIZE = 3,          // Maximizes the window
        SW_SHOW = 5,              // Shows the window in current size and position
        SW_MINIMIZE = 6,          // Minimizes the window
        SW_RESTORE = 9,           // Restores and activates the window

        // SetWindowPos Flags
        SWP_NOSIZE = 0x0001,      // Retains the current size
        SWP_NOMOVE = 0x0002,      // Retains the current position
        SWP_NOACTIVATE = 0x0010,  // Does not activate the window

        // Screen Metrics
        SM_CXSCREEN = 0,          // Screen width in pixels
        SM_CYSCREEN = 1,          // Screen height in pixels

        // OpenFileName Flags
        OFN_READONLY = 0x00000001,              // Open file as read-only
        OFN_OVERWRITEPROMPT = 0x00000002,       // Prompt before overwriting files
        OFN_HIDEREADONLY = 0x00000004,          // Hide read-only checkbox
        OFN_NOCHANGEDIR = 0x00000008,           // Never change the current directory
        OFN_SHOWHELP = 0x00000010,              // Show help button
        OFN_ENABLEHOOK = 0x00000020,            // Enable hook function
        OFN_ENABLETEMPLATE = 0x00000040,        // Use custom template for dialog
        OFN_ENABLETEMPLATEHANDLE = 0x00000080,  // Use hInstance instead of hTemplate
        OFN_NOVALIDATE = 0x00000100,            // Do not validate file names
        OFN_ALLOWMULTISELECT = 0x00000200,      // Allow multiple file selection
        OFN_EXTENSIONDIFFERENT = 0x00000400,    // Force extension to be different
        OFN_PATHMUSTEXIST = 0x00000800,         // Path must exist
        OFN_FILEMUSTEXIST = 0x00001000,         // File must exist
        OFN_CREATEPROMPT = 0x00002000,          // Prompt for file creation
        OFN_SHAREAWARE = 0x00004000,            // Share aware
        OFN_NOREADONLYRETURN = 0x00008000,      // Do not return read-only files
        OFN_NOTESTFILECREATE = 0x00010000,      // Do not test file creation
        OFN_NONETWORKBUTTON = 0x00020000,       // Hide network button
        OFN_NOLONGNAMES = 0x00040000,           // Use short names only
        OFN_EXPLORER = 0x00080000,              // Use Explorer-style dialog
        OFN_NODEREFERENCELINKS = 0x00100000,    // Do not dereference links
        OFN_LONGNAMES = 0x00200000,             // Use long names
        OFN_ENABLEINCLUDENOTIFY = 0x00400000,   // Enable include notification
        OFN_ENABLESIZING = 0x00800000,          // Enable sizing of the dialog
        OFN_DONTADDTORECENT = 0x02000000,       // Do not add to recent documents
        OFN_FORCESHOWHIDDEN = 0x10000000,       // Force showing hidden files

        // Length Limits
        MAX_TITLE_LENGTH = 256,   // Max title length for GetWindowText()

        // Taskbar Info
        TASKBAR_HEIGHT = 40;      // Taskbar default height

    /// <summary>
    /// Public constant values for SystemParametersInfo actions and flags.
    /// </summary>
    public const uint
        SPI_SETCLIENTAREAANIMATION = 0x1043,          // Enables/disables window client area animations
        SPI_SETMENUANIMATION = 0x1003,                // Enables/disables menu animations
        SPIF_SENDCHANGE = 0x2,                        // Broadcasts WM_SETTINGCHANGE after system parameter update
        PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,   // Grants limited process query rights
        TOKEN_QUERY = 0x0008,                         // Grants read access to an access token
        MOUSEEVENTF_INJECTED = 0x00000001;            // Indicates the event was injected

    /// <summary>
    /// Public constant values for special window handles.
    /// </summary>
    public static readonly nint
        HWND_TOPMOST = -1,        // Places the window above all non-topmost windows
        HWND_NOTOPMOST = -2;      // Removes the topmost attribute from a window

    /// <summary>
    /// Public constant string values for default names used in scheduling and temporary files.
    /// </summary>
    public const string
        ScheduledJobName = "ScheduledShutdown", // Default scheduled task name
        TempFileName = "iCantMailTo.txt";       // Default temporary file name

    /// <summary>
    /// GUIDs for power plan settings.
    /// </summary>
    public static readonly Guid
        GUID_MIN_POWER_SAVINGS = new("a1841308-3541-4fab-bc81-f71556f20b4a"),       // Power saver plan
        GUID_TYPICAL_POWER_SAVINGS = new("381b4222-f694-41f0-9685-ff5bb260df2e"),   // Balanced power plan
        GUID_MAX_POWER_SAVINGS = new("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c"),       // High performance plan
        GUID_ULTIMATE_POWER_SAVINGS = new("e9a42b02-d5df-448d-aa00-03f14749eb61");  // Ultimate performance plan
}