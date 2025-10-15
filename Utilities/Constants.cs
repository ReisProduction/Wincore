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
        INPUT_MOUSE = 0,        // Mouse input
        INPUT_KEYBOARD = 1,     // Keyboard input

        // XButton identifiers
        XBUTTON1 = 0x0001,     // First extended (side) mouse button
        XBUTTON2 = 0x0002,     // Second extended (side) mouse button

        // Mouse wheel constants
        WHEEL_DELTA = -120,       // Default value for one wheel notch movement

        // Keyboard messages
        WM_KEYDOWN = 0x0100,   // Key pressed message
        WM_KEYUP = 0x0101,     // Key released message

        // Keyboard event flags
        KEYEVENTF_EXTENDEDKEY = 0x0001,     // Extended key (e.g. right ALT, right CTRL)
        KEYEVENTF_KEYUP = 0x0002,           // Key release event
        KEYEVENTF_SCANCODE = 0x0008,        // Use scan code instead of virtual-key code

        // Mouse event flags
        MOUSEEVENTF_WHEEL = 0x0800,       // Vertical wheel movement
        MOUSEEVENTF_HWHEEL = 0x01000,     // Horizontal wheel movement
        MOUSEEVENTF_XDOWN = 0x0080,       // X button pressed
        MOUSEEVENTF_XUP = 0x0100,         // X button released
        MOUSEEVENTF_MOVE = 0x0001,        // Mouse movement
        MOUSEEVENTF_LEFTDOWN = 0x0002,    // Left button pressed
        MOUSEEVENTF_LEFTUP = 0x0004,      // Left button released
        MOUSEEVENTF_RIGHTDOWN = 0x0008,   // Right button pressed
        MOUSEEVENTF_RIGHTUP = 0x0010,     // Right button released
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,  // Middle button pressed
        MOUSEEVENTF_MIDDLEUP = 0x0040,    // Middle button released
        MOUSEEVENTF_ABSOLUTE = 0x8000,    // Absolute cursor position (not relative)

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
        TOKEN_QUERY = 0x0008;                         // Grants read access to an access token

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
}