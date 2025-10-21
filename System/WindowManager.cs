using System.Drawing;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Window management utility class.
/// </summary>
public static class WindowManager
{
    /// <inheritdoc cref="ProcessManager.Start(string, string, bool, int, bool, bool, bool, bool, bool)"/>
    public static int Start(string path, string args = "",
        bool waitForExit = false, int waitMilisecond = 3210,
        bool isHidden = false, bool noWindow = false,
        bool useShell = true, bool runAs = false) =>
        ProcessManager.Start(path, args, waitForExit,
        waitMilisecond, isHidden, noWindow, useShell, runAs);
    /// <summary>
    /// Title of the window given its handle.
    /// Throws an exception if the hWnd is 0 and throwIfNull is true.
    /// </summary>
    public static string FindTitle(nint hWnd, bool throwIfNull = true)
    {
        if (hWnd is 0)
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(hWnd), "Window handle is zero.");
            return string.Empty;
        }
        StringBuilder sb = new(MAX_TITLE_LENGTH);
        return GetWindowText(hWnd, sb, sb.Capacity) > 0 ? sb.ToString() : string.Empty;
    }
    /// <summary>
    /// Find window handle based on WindowInfo. Optionally fallback to foreground window.
    /// Throws an exception if the name is null or whitespace and throwIfNull is true.
    /// </summary>
    public static nint FindHandle(WindowInfo info, bool foregroundWindow = false, bool throwIfNull = true)
    {
        if (info is null)
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(info), "WindowInfo is null.");
            return 0;
        }
        var hWnd = info.Handle;
        if (hWnd is 0)
            hWnd = FindHandle(new ProcessInfo(info.Id, info.ProcessName), foregroundWindow, throwIfNull);
        return hWnd;
    }
    /// <summary>
    /// Find window handle based on ProcessInfo. Optionally fallback to foreground window.
    /// Throws an exception if the name is null or whitespace and throwIfNull is true.
    /// </summary>
    public static nint FindHandle(ProcessInfo info, bool foregroundWindow = false, bool throwIfNull = true)
    {
        if (info is null)
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(info), "ProcessInfo is null.");
            return 0;
        }
        return FindHandle(info.Id, foregroundWindow, throwIfNull);
    }
    /// <summary>
    /// Find window handle based on process name. Optionally fallback to foreground window.
    /// Throws an exception if the name is null or whitespace and throwIfNull is true.
    /// </summary>
    public static nint FindHandle(string title, bool foregroundWindow = false, bool throwIfNull = true)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(title), "Process name is null or empty.");
            return 0;
        }
        var id = FindId(title, throwIfNull);
        return FindHandle(id, foregroundWindow, throwIfNull);
    }
    /// <summary>
    /// Find window handle based on process ID. Optionally fallback to foreground window.
    /// Throws an exception if the name is null or whitespace and throwIfNull is true.
    /// </summary>
    public static nint FindHandle(int pid, bool foregroundWindow = false, bool throwIfNull = true)
    {
        if (pid is 0)
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(pid), "Process ID is zero.");
            return 0;
        }
        nint handle;
        try { handle = Process.GetProcessById(pid).MainWindowHandle; }
        catch { handle = 0; }
        if (handle is 0 && foregroundWindow) handle = GetForegroundWindow();
        if (handle is 0 && throwIfNull) throw new ArgumentNullException($"Process (PID={pid}) window not found.");
        return handle;
    }
    /// <inheritdoc cref="ProcessManager.FindId(ProcessInfo, bool)"/>
    public static int FindId(ProcessInfo info, bool throwIfNull = true) => ProcessManager.FindId(info, throwIfNull);
    /// <inheritdoc cref="ProcessManager.FindId(string, bool)"/>
    public static int FindId(string name, bool throwIfNull = true) => ProcessManager.FindId(name, throwIfNull);
    /// <inheritdoc cref="ProcessManager.FindName(int)"/>
    public static string FindName(int pid) => ProcessManager.FindName(pid);
    /// <inheritdoc cref="ProcessManager.StartTime(int)"/>
    public static DateTime StartTime(int pid) => ProcessManager.StartTime(pid);
    /// <inheritdoc cref="ProcessManager.IsRunning(int)"/>
    public static bool IsRunning(int pid) => ProcessManager.IsRunning(pid);
    /// <inheritdoc cref="ProcessManager.IsResponding(int)"/>
    public static bool IsResponding(int pid) => ProcessManager.IsResponding(pid);
    /// <inheritdoc cref="ProcessManager.IsAdmin(int)"/>
    public static bool IsAdmin(int pid) => ProcessManager.IsAdmin(pid);
    /// <inheritdoc cref="ProcessManager.Refresh(int)"/>
    public static bool Refresh(int pid) => ProcessManager.Refresh(pid);
    /// <summary>
    /// Hides the window from the taskbar.
    /// </summary>
    public static void HideFromTaskbar(nint hWnd)
    {
        int ex = GetWindowLong(hWnd, GWL_EXSTYLE);
        ex &= ~WS_EX_APPWINDOW;
        ex |= WS_EX_TOOLWINDOW;
        _ = SetWindowLong(hWnd, GWL_EXSTYLE, ex);
    }
    /// <summary>
    /// Shows the window in the taskbar.
    /// </summary>
    public static void ShowInTaskbar(nint hWnd)
    {
        int ex = GetWindowLong(hWnd, GWL_EXSTYLE);
        ex &= ~WS_EX_TOOLWINDOW;
        ex |= WS_EX_APPWINDOW;
        _ = SetWindowLong(hWnd, GWL_EXSTYLE, ex);
    }
    /// <summary>
    /// Enables or disables the shadow effect for the specified window.
    /// </summary>
    public static void EnableShadow(nint hWnd, bool enable = true)
    {
        try
        {
            int attr = enable ? 2 : 0;
            _ = DwmSetWindowAttribute(hWnd, 2, [attr], sizeof(int));
        }
        catch { }
    }
    /// <summary>
    /// Toggles the window animations for opening and closing windows.
    /// </summary>
    public static void EnterAnimations(bool enable) => SystemParametersInfo(SPI_SETCLIENTAREAANIMATION, 0, enable ? 1 : 0, SPIF_SENDCHANGE);
    /// <summary>
    /// Toggles the menu animations for opening and closing menus.
    /// </summary>
    public static void MenuAnimation(bool enable) => SystemParametersInfo(SPI_SETMENUANIMATION, 0, enable ? 1 : 0, SPIF_SENDCHANGE);
    /// <summary>
    /// Moves the window to the specified position and size.
    /// </summary>
    public static bool Move(nint hWnd, int x, int y, bool repaint = true)
    {
        GetWindowRect(hWnd, out RECT r);
        return MoveWindow(hWnd, x, y, r.Right - r.Left, r.Bottom - r.Top, repaint);
    }
    /// <summary>
    /// Resizes the window to the specified width and height, keeping its current position.
    /// </summary>
    public static bool Resize(nint hWnd, int w, int h, bool repaint = true)
    {
        GetWindowRect(hWnd, out RECT r);
        return MoveWindow(hWnd, r.Left, r.Top, w, h, repaint);
    }
    /// <summary>
    /// Moves and resizes the window to the specified position and size.
    /// </summary>
    public static bool MoveAndResize(nint hWnd, int x, int y,
        int w, int h, bool repaint = true) =>
        MoveWindow(hWnd, x, y, w, h, repaint);
    /// <summary>
    /// Is the window minimized.
    /// </summary>
    public static bool IsMinimized(nint hWnd) => IsIconic(hWnd);
    /// <summary>
    /// Is the window in its normal (restored) state.
    /// </summary>
    public static bool IsRestored(nint hWnd) => !IsIconic(hWnd) && !IsZoomed(hWnd);
    /// <summary>
    /// Is the window maximized.
    /// </summary>
    public static bool IsMaximized(nint hWnd) => IsZoomed(hWnd);
    /// <summary>
    /// Is the window in fullscreen mode.
    /// </summary>
    public static bool IsFullscreen(nint hWnd)
    {
        GetWindowRect(hWnd, out RECT rect);
        int sw = GetSystemMetrics(SM_CXSCREEN);
        int sh = GetSystemMetrics(SM_CYSCREEN);
        return rect.Left <= 0 && rect.Top <= 0 && rect.Right >= sw && rect.Bottom >= sh;
    }
    /// <summary>
    /// IsWindowedFullscreen - Checks if the window is in windowed fullscreen mode (maximized but not covering the taskbar).
    /// </summary>
    public static bool IsWindowedFullscreen(nint hWnd)
    {
        if (!IsZoomed(hWnd)) return false;
        GetWindowRect(hWnd, out RECT r);
        return r.Top is 0 && r.Left is 0;
    }
    /// <summary>
    /// Set the window to fullscreen or windowed mode.
    /// </summary>
    public static bool SetFullscreen(nint hWnd, bool fullscreen)
    {
        if (fullscreen)
            _ = SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        else
            _ = SetWindowLong(hWnd, GWL_STYLE, WS_OVERLAPPEDWINDOW | WS_VISIBLE);
        return Show(hWnd);
    }
    /// <summary>
    /// Sets the window to fullscreen mode, covering the entire screen.
    /// </summary>
    public static bool SetWindowedFullscreen(nint hWnd)
    {
        GetWindowRect(hWnd, out RECT _);
        int sw = GetSystemMetrics(SM_CXSCREEN);
        int sh = GetSystemMetrics(SM_CYSCREEN);
        return MoveWindow(hWnd, 0, 0, sw, sh - TASKBAR_HEIGHT, true);
    }
    /// <summary>
    /// Centers the window on the screen.
    /// </summary>
    public static void CenterOnScreen(nint hWnd)
    {
        GetWindowRect(hWnd, out RECT r);
        int w = r.Right - r.Left, h = r.Bottom - r.Top;
        int x = (GetSystemMetrics(SM_CXSCREEN) - w) / 2;
        int y = (GetSystemMetrics(SM_CYSCREEN) - h) / 2;
        MoveWindow(hWnd, x, y, w, h, true);
    }
    /// <summary>
    /// Gets the bounds of the specified window as a Rectangle.
    /// </summary>
    public static Rectangle GetBounds(nint hWnd)
    {
        GetWindowRect(hWnd, out RECT r);
        return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
    }
    /// <summary>
    /// Minimizes the specified window.
    /// </summary>
    public static bool Minimize(nint hWnd) => ShowWindow(hWnd, SW_MINIMIZE);
    /// <summary>
    /// Maximizes the specified window.
    /// </summary>
    public static bool Maximize(nint hWnd) => ShowWindow(hWnd, SW_MAXIMIZE);
    /// <summary>
    /// Restores the specified window to its previous size and position.
    /// </summary>
    public static bool Restore(nint hWnd) => ShowWindow(hWnd, SW_RESTORE);
    /// <summary>
    /// Hides the specified window.
    /// </summary>
    public static bool Hide(nint hWnd) => ShowWindow(hWnd, SW_HIDE);
    /// <summary>
    /// Shows the specified window.
    /// </summary>
    public static bool Show(nint hWnd) => ShowWindow(hWnd, SW_SHOW);
    /// <inheritdoc cref="ProcessManager.CommandLine(int)"/>
    public static string CommandLine(int pid) => ProcessManager.CommandLine(pid);
    /// <inheritdoc cref="ProcessManager.Description(int)"/>
    public static string Description(int pid) => ProcessManager.Description(pid);
    /// <inheritdoc cref="ProcessManager.FileVersion(int)"/>
    public static string FileVersion(int pid) => ProcessManager.FileVersion(pid);
    /// <inheritdoc cref="ProcessManager.MainModule(int)"/>
    public static string MainModule(int pid) => ProcessManager.MainModule(pid);
    /// <inheritdoc cref="ProcessManager.Company(int)"/>
    public static string Company(int pid) => ProcessManager.Company(pid);
    /// <inheritdoc cref="ProcessManager.SessionUser(int)"/>
    public static string SessionUser(int pid) => ProcessManager.SessionUser(pid);
    /// <inheritdoc cref="ProcessManager.Owner(int)"/>
    public static string Owner(int pid) => ProcessManager.Owner(pid);
    /// <inheritdoc cref="ProcessManager.MemoryUsage(int)"/>
    public static long MemoryUsage(int pid) => ProcessManager.MemoryUsage(pid);
    /// <inheritdoc cref="ProcessManager.CpuUsage(string, int)"/>
    public static float CpuUsage(string processName, int sleepTime = 123) => ProcessManager.CpuUsage(processName, sleepTime);
    /// <inheritdoc cref="ProcessManager.ExecutablePath(int, int, int)"/>
    public static string ExecutablePath(int pid, int sbCapacity = 1024, int queryFlag = 0) => ProcessManager.ExecutablePath(pid, sbCapacity, queryFlag);
    /// <summary>
    /// Brings the specified window to the front and optionally shows it.
    /// </summary>
    public static (bool, bool) BringToFront(nint hWnd, bool showWindow = true, bool bringToFront = true) =>
        (showWindow && Show(hWnd), bringToFront && SetForegroundWindow(hWnd));
    /// <summary>
    /// Toggles the topmost state of the specified window and optionally shows it and brings it to the front.
    /// </summary>
    public static (bool, bool) TopMost(nint hWnd, bool showWindow = true, bool bringToFront = true, bool topMost = true) =>
        (showWindow && Show(hWnd), bringToFront && SetWindowPos(hWnd, topMost ? HWND_TOPMOST : HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE));
    /// <summary>
    /// Closes the main window of the process with the specified ID. If not closed, it can optionally kill the process.
    /// </summary>
    public static bool CloseMainWindow(int pid, bool waitForExit = true, bool killIfNotClose = true, int waitMilisecond = 3210)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            process.CloseMainWindow();
            if (waitForExit)
                process.WaitForExit(waitMilisecond);
            if (!process.HasExited)
            {
                if (killIfNotClose)
                    return Kill(pid, waitForExit, waitMilisecond);
                return false;
            }
            return true;
        }
        catch (Exception ex) { throw new InvalidOperationException($"Unable to kill process with PID {pid}.", ex); }
    }
    /// <inheritdoc cref="ProcessManager.Close(int, bool, bool, int)"/>
    public static bool Close(int pid, bool waitForExit = true, bool killIfNotClose = true, int waitMilisecond = 3210) => ProcessManager.Close(pid, waitForExit, killIfNotClose, waitMilisecond);
    /// <inheritdoc cref="ProcessManager.Kill(int, bool, int)"/>
    public static bool Kill(int pid, bool waitForExit = true, int waitMilisecond = 3210) => ProcessManager.Kill(pid, waitForExit, waitMilisecond);
}