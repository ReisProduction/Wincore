using ReisProduction.Windelay.Utilities;
using Windows.System;
namespace ReisProduction.Wincore.Utilities;
public record ProcessInfo(
    int Id = 0,
    string ProcessName = "") : IProcessInfo;
public record WindowInfo(
    int Id = 0,
    nint Handle = 0,
    string Title = "",
    string ProcessName = "") : IWindowInfo;
public record DiskInfo(
    string Name,
    long TotalGB,
    long FreeGB,
    string FileSystem,
    DriveType Type
) : IDiskInfo
{
    public long UsedGB => TotalGB - FreeGB;
    public double PercentUsed => TotalGB > 0 ? (double)UsedGB / TotalGB * 100 : 0;
}
public record InputSequence(IReadOnlyList<InputStep> Steps);
public record InputStep(
    IInputEvent Action,
    DelayAction? Delay = default,
    WindowInfo Info = default!
);
public record KybdEvent(
    VirtualKey[] Keys,
    bool[] UseScanCode,
    bool[] UseUnicode,
    bool[] IsExtendedKey,
    WindowsMessageType[] Messages,
    WindowInfo WindowInfo = default!,
    int Time = 0
) : IKybdEvent
{
    public static KybdEvent Create(VirtualKey[] keys, WindowsMessageType[] messages,
        bool useScanCode = false, bool useUnicode = false, bool isExtendedKey = false,
        WindowInfo windowInfo = default!, int time = 0) =>
    new(
        Keys: keys,
        UseScanCode: [.. Enumerable.Repeat(useScanCode, keys.Length)],
        UseUnicode: [.. Enumerable.Repeat(useUnicode, keys.Length)],
        IsExtendedKey: [.. Enumerable.Repeat(isExtendedKey, keys.Length)],
        Messages: messages,
        WindowInfo: windowInfo,
        Time: time
    );
}
public record ButtonAction(
    ButtonType[] Buttons,
    WindowsMessageType[] Messages,
    WindowInfo WindowInfo = default!
) : IButtonEvent;
public record ScrollAction(
    ScrollType[] ScrollTypes,
    int[] ScrollAmount,
    WindowInfo WindowInfo = default!
) : IScrollEvent;
public record MoveAction(
    MoveType[] Moves,
    int[] DeltaX,
    int[] DeltaY,
    WindowInfo WindowInfo = default!
) : IMoveEvent;
public record MouseEvent(
    MouseEventType Events,
    int XButton = 1,
    int Wheel = 0,
    int Dx = 0,
    int Dy = 0,
    int DwData = 0,
    int DwExtraInfo = 0,
    int Time = 0,
    WindowInfo WindowInfo = default!
) : IMouseEvent;
#if WINUI || WINDOWS_APP || WINRT
public record MouseAction(
    Windows.UI.Input.Preview.Injection.
    InjectedInputMouseOptions[] Options,
    uint[] MouseData,
    int[] DeltaX,
    int[] DeltaY,
    WindowInfo WindowInfo = default!
) : IMouseAction;
public record GamepadAction(
    Windows.Gaming.Input.
    GamepadButtons Buttons,
    byte LeftTrigger = 0,
    byte RightTrigger = 0,
    short LeftThumbstickX = 0,
    short LeftThumbstickY = 0,
    short RightThumbstickX = 0,
    short RightThumbstickY = 0,
    WindowInfo WindowInfo = default!
) : IGamepadAction;
public record PenAction(
    Windows.UI.Input.Preview.Injection.
    InjectedInputPointerOptions Options,
    Windows.UI.Input.Preview.Injection.
    InjectedInputPoint Point,
    int Pressure = 2000,
    int TiltX = 0,
    int TiltY = 0,
    double Rotation = 0,
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenButtons PenButtons =
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenButtons.None,
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenParameters PenParameters =
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenParameters.Pressure |
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenParameters.Rotation |
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenParameters.TiltX |
    Windows.UI.Input.Preview.Injection.
    InjectedInputPenParameters.TiltY,
    WindowInfo WindowInfo = default!
) : IPenAction;
public record TouchAction(
    Windows.UI.Input.Preview.Injection.
    InjectedInputPointerOptions Options,
    Windows.UI.Input.Preview.Injection.
    InjectedInputPoint Point,
    WindowInfo WindowInfo = default!
) : ITouchAction;
#endif