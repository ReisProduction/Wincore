using System.Runtime.InteropServices;
using System.ComponentModel;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Pointer-related utilities for mouse and cursor operations.
/// </summary>
public static class Pointer
{
    public static uint SendMouseInput(InputType button, bool press, bool release)
    {


        INPUT input = new()
        {
            type = INPUT_MOUSE,
            u = new INPUTUNION
            {
                mi = new MOUSEINPUT
                {
                    dwFlags = isLeftClick
                        ? (isDown ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_LEFTUP)
                        : (isDown ? MOUSEEVENTF_RIGHTDOWN : MOUSEEVENTF_RIGHTUP),
                    time = 0,
                    dwExtraInfo = nint.Zero
                }
            }
        };
        return SendInput(1, [input], Marshal.SizeOf<INPUT>());
    }
    /// <summary>
    /// Sends a mouse event (button press/release) for the specified button type.
    /// </summary>
    public static void SendMouseEvent(ButtonType button, bool press, bool release)
    {
        var ev = button switch
        {
            ButtonType.LeftButton => (press ? MouseEvent.LEFTDOWN : 0) | (release ? MouseEvent.LEFTUP : 0),
            ButtonType.RightButton => (press ? MouseEvent.RIGHTDOWN : 0) | (release ? MouseEvent.RIGHTUP : 0),
            ButtonType.MiddleButton => (press ? MouseEvent.MIDDLEDOWN : 0) | (release ? MouseEvent.MIDDLEUP : 0),
            ButtonType.XButton1 or ButtonType.XButton2 => (press ? MouseEvent.XDOWN : 0) | (release ? MouseEvent.XUP : 0),
            _ => MouseEvent.None
        };
        if (ev is not MouseEvent.None) mouse_event(ev, 0, 0, 0, 0);
    }
    /// <summary>
    /// Gets the current position of the cursor on the screen.
    /// </summary>
    public static POINT GetCursorPos() => NativeMethods.GetCursorPos(out POINT point) ? point :
        throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to get cursor position");
    /// <summary>
    /// Sets the position of the cursor on the screen to the specified coordinates.
    /// </summary>
    public static void SetCursorPos(int x, int y)
    {
        if (!NativeMethods.SetCursorPos(x, y))
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to set cursor position");
    }
}