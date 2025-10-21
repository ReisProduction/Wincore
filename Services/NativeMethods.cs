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
    internal static extern bool CredRead(string target, int type, int reservedFlag, out nint credentialPtr);
    [DllImport("advapi32", EntryPoint = "CredFree", SetLastError = true)]
    internal static extern void CredFree([In] nint cred);
    [DllImport("advapi32", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool CredDelete(string target, int type, int reservedFlag);
    [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool OpenProcessToken(nint ProcessHandle, uint DesiredAccess, out nint TokenHandle);
    #endregion
    #region Dwmapi
    [DllImport("dwmapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int DwmSetWindowAttribute(nint hwnd, int attr, int[] attrValue, int attrSize);
    #endregion
    #region Uxtheme
    [DllImport("uxtheme.dll", EntryPoint = "#95")]
    internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);
    [DllImport("uxtheme.dll", EntryPoint = "#96")]
    internal static extern uint GetImmersiveColorTypeFromName(nint pName);
    [DllImport("uxtheme.dll", EntryPoint = "#98")]
    internal static extern int GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);
    #endregion
    #region BthpropsCpl
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothEnableDiscovery(nint hwndCaller, bool fEnabled);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothEnableIncomingConnections(nint hwndCaller, bool fEnabled);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern nint BluetoothFindFirstRadio(ref BLUETOOTH_FIND_RADIO_PARAMS pbtfrp, out nint phRadio);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool BluetoothFindNextRadio(nint hFind, out nint phRadio);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool BluetoothFindRadioClose(nint hFind);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothGetRadioInfo(nint hRadio, ref BLUETOOTH_RADIO_INFO pRadioInfo);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool BluetoothIsDiscoverable(nint hRadio);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool BluetoothIsConnectable(nint hRadio);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothAuthenticateDevice(nint hwndParent, nint hRadio, ref BLUETOOTH_DEVICE_INFO pbtdi, StringBuilder pszPasskey, uint ulPasskeyLength);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothRemoveDevice(ref Guid pAddress);
    [DllImport("bthprops.cpl", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int BluetoothSetServiceState(nint hRadio, ref BLUETOOTH_DEVICE_INFO pbtdi, ref Guid pGuidService, uint dwServiceFlags);
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
    internal static extern uint MapVirtualKey(uint uCode, uint uMapType);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint GetMessageExtraInfo();
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool PostMessage(nint hWnd, uint Msg, nint wParam, nint lParam);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint SendMessage(nint hWnd, uint Msg, nint wParam, nint lParam);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern void mouse_event(MouseEventType dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
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
    internal static extern bool ShowCursor(bool bShow);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool GetCursorPos(out POINT lpPoint);
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);
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