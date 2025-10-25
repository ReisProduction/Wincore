using System.Runtime.InteropServices;
using System.ComponentModel;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Pointer utility class for handling pointer input and actions.
/// </summary>
public static class Pointer
{
    /// <summary>
    /// Is the specified mouse button currently pressed/down/held?
    /// </summary>
    public static bool IsDown(ButtonType button) => Input.IsKeyDown(
        InputConverter.ToVirtualKey(InputConverter.ToInput(button)));
    /// <summary>
    /// Is the specified mouse button currently not pressed/up/released?
    /// </summary>
    public static bool IsUp(ButtonType button) => !IsDown(button);
    /// <summary>
    /// Is the specified mouse button currently pressed (down) at the hardware level?
    /// </summary>
    public static bool IsHardwareDown(ButtonType button) => Input.IsHardwareKeyDown(
        InputConverter.ToVirtualKey(InputConverter.ToInput(button)));
    /// <summary>
    /// Is the specified mouse button currently not pressed (up) at the hardware level?
    /// </summary>
    public static bool IsHardwareUp(ButtonType button) => !IsHardwareDown(button);
    /// <summary>
    /// Sends a mouse input with SendInput API.
    /// </summary>
    public static uint SendInputs(MouseEvent mouse)
    {
        int mouseData = 0;
        if (mouse.Events.HasFlag(MouseEventType.WHEEL) ||
            mouse.Events.HasFlag(MouseEventType.HWHEEL))
            mouseData = mouse.Wheel;
        else if (mouse.Events.HasFlag(MouseEventType.XDOWN) ||
            mouse.Events.HasFlag(MouseEventType.XUP))
            mouseData = mouse.XButton;
        int dx = mouse.Events.HasFlag(MouseEventType.MOVE) ||
            mouse.Events.HasFlag(MouseEventType.ABSOLUTE) ? mouse.Dx : 0,
            dy = mouse.Events.HasFlag(MouseEventType.MOVE) ||
            mouse.Events.HasFlag(MouseEventType.ABSOLUTE) ? mouse.Dy : 0;
        INPUT input = new()
        {
            type = INPUT_MOUSE,
            u = new()
            {
                mi = new()
                {
                    dx = dx, dy = dy,
                    mouseData = mouseData,
                    dwFlags = (int)mouse.Events,
                    dwExtraInfo = (int)Input.GetMsgExtraInfo(),
                    time = mouse.Time
                }
            }
        };
        BringToFront(FindHandle(mouse.WindowInfo, false, false));
        return SendInput(1, [input], Marshal.SizeOf<INPUT>());
    }
    /// <summary>
    /// Sends a mouse event with mouse_event API.
    /// </summary>
    public static void SendEvents(MouseEvent mouse)
    {
        BringToFront(FindHandle(mouse.WindowInfo, false, false));
        mouse_event(mouse.Events, mouse.Dx, mouse.Dy,
            mouse.DwData, mouse.DwExtraInfo);
    }
    /// <summary>
    /// Clicks the mouse at the specified point using the specified mouse button with SendInput API.
    /// If point is null, uses the current cursor position.
    /// </summary>
    public static uint Click(MouseEventType button, POINT? point = null, WindowInfo? window = null)
    {
        if (point is not null)
            SetPos(point.Value);
        return SendInputs(new(Events: button, WindowInfo: window!));
    }
    /// <summary>
    /// Clicks the mouse at the specified point using the specified mouse button with mouse_event API.
    /// If point is null, uses the current cursor position.
    /// </summary>
    public static void ClickEvent(MouseEventType button, POINT? point = null, WindowInfo? window = null)
    {
        if (point is not null)
            SetPos(point.Value);
        SendEvents(new(Events: button, WindowInfo: window!));
    }
    /// <summary>
    /// Drags the mouse from the start point to the end point using the specified mouse button.
    /// If start is null, uses the current cursor position as the start point.
    /// </summary>
    public static uint Drag(MouseEventType button, POINT end, POINT? start = null, WindowInfo? window = null)
    {
        SetPos(start ?? GetPos(out bool _));
        SendInputs(new(Events: button));
        SetPos(end);
        return SendInputs(new(Events: button, WindowInfo: window!));
    }
    /// <summary>
    /// Drags the mouse from the start point to the end point using the specified mouse button.
    /// If start is null, uses the current cursor position as the start point.
    /// </summary>
    public static void DragEvent(MouseEventType button, POINT end, POINT? start = null, WindowInfo? window = null)
    {
        SetPos(start ?? GetPos(out bool _));
        SendEvents(new(Events: button));
        SetPos(end);
        SendEvents(new(Events: button, WindowInfo: window!));
    }
    /// <summary>
    /// Scrolls the mouse wheel in the specified direction and amount with SendInput API.
    /// </summary>
    public static uint Scroll(WindowInfo window, ScrollType scrollType, int amount) =>
    SendInputs(new(scrollType switch
    {
        ScrollType.MouseScrollUp => MouseEventType.WHEEL,
        ScrollType.MouseScrollDown => MouseEventType.WHEEL,
        ScrollType.MouseScrollLeft => MouseEventType.HWHEEL,
        ScrollType.MouseScrollRight => MouseEventType.HWHEEL,
        _ => 0
    }, Wheel: scrollType switch
    {
        ScrollType.MouseScrollUp => amount,
        ScrollType.MouseScrollDown => -amount,
        ScrollType.MouseScrollLeft => -amount,
        ScrollType.MouseScrollRight => amount,
        _ => 0
    }, WindowInfo: window));
    /// <summary>
    /// Scrolls the mouse wheel in the specified direction and amount with mouse_event API.
    /// </summary>
    public static void ScrollEvent(WindowInfo window, ScrollType scrollType, int amount) =>
    SendEvents(new(scrollType switch
    {
        ScrollType.MouseScrollUp => MouseEventType.WHEEL,
        ScrollType.MouseScrollDown => MouseEventType.WHEEL,
        ScrollType.MouseScrollLeft => MouseEventType.HWHEEL,
        ScrollType.MouseScrollRight => MouseEventType.HWHEEL,
        _ => 0
    }, Wheel: scrollType switch
    {
        ScrollType.MouseScrollUp => amount,
        ScrollType.MouseScrollDown => -amount,
        ScrollType.MouseScrollLeft => -amount,
        ScrollType.MouseScrollRight => amount,
        _ => 0
    }, WindowInfo: window));
    /// <summary>
    /// Shows or hides the cursor based on the 'show' parameter.
    /// Changes the visibility state of the cursor.
    /// </summary>
    public static bool Visibility(bool show) => ShowCursor(show);
    /// <summary>
    /// Gets the current position of the cursor on the screen.
    /// </summary>
    public static POINT GetPos(out bool success) => GetCursorPos(out POINT point) ? (success = true, point).point :
        throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to get cursor position");
    /// <inheritdoc cref="GetPos(out bool)"/>
    public static bool GetPos(out POINT point) => GetCursorPos(out point);
    /// <summary>
    /// Sets the position of the cursor on the screen to the specified coordinates.
    /// </summary>
    public static bool SetPos(POINT point) => SetCursorPos(point.X, point.Y);
    /// <summary>
    /// Sets the position of the cursor relative to a specified window and anchor point.
    /// </summary>
    public static bool SetPosAnchor(WindowInfo window, WindowAnchor anchor)
    {
        var hWnd = FindHandle(window, false, false);
        if (hWnd is 0) return false;
        GetWindowRect(hWnd, out RECT rect);
        return SetPos(anchor switch
        {
            WindowAnchor.Center => new((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2),
            WindowAnchor.TopLeft => new(rect.Left + 5, rect.Top + 5),
            WindowAnchor.TopRight => new(rect.Right - 5, rect.Top + 5),
            WindowAnchor.TopCenter => new((rect.Left + rect.Right) / 2, rect.Top + 5),
            WindowAnchor.BottomLeft => new(rect.Left + 5, rect.Bottom - 5),
            WindowAnchor.BottomRight => new(rect.Right - 5, rect.Bottom - 5),
            WindowAnchor.BottomCenter => new((rect.Left + rect.Right) / 2, rect.Bottom - 5),
            _ => new((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2)
        });
    }
    /// <summary>
    /// Moves the cursor by the specified relative offset.
    /// </summary>
    public static bool MoveRelative(POINT dp)
    {
        GetPos(out POINT p);
        return SetPos(new(p.X + dp.X, p.Y + dp.Y));
    }
}