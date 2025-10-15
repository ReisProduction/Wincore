﻿#if WINUI || WINDOWS_APP || WINRT
using Windows.UI.Input.Preview.Injection;
using Windows.Gaming.Input;
#endif
using Windows.Graphics;
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
    IInputAction Action,
    DelayAction? Delay = default
);
public record KybdAction<T>(
    T[] Keys,
    bool[] States,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IKybdAction<T>;
public record ButtonAction(
    bool[] States,
    ButtonType[] Buttons,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IMouseButton;
public record ScrollAction(
    ScrollType[] ScrollTypes,
    int[] ScrollAmount,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IScrollAction;
public record MoveAction(
    MoveType[] Moves,
    PointInt32[] CursorPoints
) : IMoveAction;
#if WINUI || WINDOWS_APP || WINRT
public record MouseAction(
    InjectedInputMouseOptions[] Options,
    uint[] MouseData,
    int[] DeltaX,
    int[] DeltaY,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IMouseAction;
public record GamepadAction(
    GamepadButtons Buttons,
    byte LeftTrigger = 0,
    byte RightTrigger = 0,
    short LeftThumbstickX = 0,
    short LeftThumbstickY = 0,
    short RightThumbstickX = 0,
    short RightThumbstickY = 0,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IGamepadAction;
public record PenAction(
    InjectedInputPointerOptions Options,
    InjectedInputPoint Point,
    int Pressure = 2000,
    int TiltX = 0,
    int TiltY = 0,
    double Rotation = 0,
    InjectedInputPenButtons PenButtons = InjectedInputPenButtons.None,
    InjectedInputPenParameters PenParameters = InjectedInputPenParameters.Pressure |
    InjectedInputPenParameters.Rotation | InjectedInputPenParameters.TiltX | InjectedInputPenParameters.TiltY,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : IPenAction;
public record TouchAction(
    InjectedInputPointerOptions Options,
    InjectedInputPoint Point,
    nint WindowhWnd = 0,
    string WindowTitle = ""
) : ITouchAction;
#endif