using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Services;
internal static class NativeMethods
{
    #region Kernel32
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern long WritePrivateProfileString(string section, string? key, string? val, string filePath);
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetPrivateProfileString(string section, string? key, string? def, StringBuilder retVal, int size, string filePath);
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool QueryFullProcessImageName(nint hProcess, int flags, StringBuilder exeName, ref int size);
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool CloseHandle(nint hObject);
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
    #endregion
    #region Advapi32
    [DllImport("advapi32", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool CredWrite([In] ref NativeCredential userCredential, [In] uint flags);
    [DllImport("advapi32", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool CredRead(string target, int type, int reservedFlag, out IntPtr credentialPtr);
    [DllImport("advapi32", EntryPoint = "CredFree", SetLastError = true)]
    internal static extern void CredFree([In] IntPtr cred);
    [DllImport("advapi32", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool CredDelete(string target, int type, int reservedFlag);
    [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool OpenProcessToken(nint ProcessHandle, uint DesiredAccess, out nint TokenHandle);
    #endregion
    #region Dwmapi
    [DllImport("dwmapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int DwmSetWindowAttribute(nint hwnd, int attr, int[] attrValue, int attrSize);
    #endregion
    #region User32
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int MessageBoxW(nint hWnd, string text, string caption, uint type);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetWindowLong(nint hWnd, int nIndex);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool GetWindowRect(nint hWnd, out RECT rect);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern short GetKeyState(int nVirtKey);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern short GetAsyncKeyState(ushort key);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool PostMessage(nint hWnd, uint Msg, nint wParam, nint lParam);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint SendMessage(nint hWnd, uint Msg, nint wParam, nint lParam);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern void mouse_event(MouseEvent dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool IsIconic(nint hWnd);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool IsZoomed(nint hWnd);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SystemParametersInfo(uint uiAction, uint uiParam, int pvParam, uint fWinIni);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool GetCursorPos(out POINT lpPoint);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint GetMessageExtraInfo();
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint GetForegroundWindow();
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetForegroundWindow(nint hWnd);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool ShowWindow(nint hWnd, int nCmdShow);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint FindWindow(string? lpClassName, string lpWindowName);
    #endregion
}