using Windows.System;
namespace ReisProduction.Wincore.Utilities;
public interface IProcessInfo
{
    int Id { get; }
    string ProcessName { get; }
}
public interface  IWindowInfo : IProcessInfo
{
    nint Handle { get; }
    string Title { get; }
}
public interface IDiskInfo
{
    string Name { get; }
    long TotalGB { get; }
    long FreeGB { get; }
    long UsedGB { get; }
    double PercentUsed { get; }
    string FileSystem { get; }
    DriveType Type { get; }
}
public interface IInputEvent
{
    WindowInfo WindowInfo { get; }
}
public interface IPressEvent : IInputEvent
{
    WindowsMessageType[] Messages { get; }
}
public interface IKybdEvent : IPressEvent
{
    VirtualKey[] Keys { get; }
    bool[] UseScanCode { get; }
    bool[] IsExtendedKey { get; }
    int Time { get; }
}
public interface IScrollEvent : IInputEvent
{
    ScrollType[] ScrollTypes { get; }
    int[] ScrollAmount { get; }
}
public interface IButtonEvent : IPressEvent
{
    ButtonType[] Buttons { get; }
}
public interface IMoveEvent : IInputEvent
{
    MoveType[] Moves { get; }
    int[] DeltaX { get; }
    int[] DeltaY { get; }
}
public interface IMouseEvent : IInputEvent
{
    MouseEventType Events { get; }
    int XButton { get; }
    int Wheel { get; }
    int Dx { get; }
    int Dy { get; }
    int DwData { get; }
    int DwExtraInfo { get; }
    int Time { get; }
}
#if WINUI || WINDOWS_APP || WINRT
public interface IMouseAction : IInputEvent
{
    Windows.UI.Input.Preview.Injection.
    InjectedInputMouseOptions[] Options { get; }
    uint[] MouseData { get; }
    int[] DeltaX { get; }
    int[] DeltaY { get; }
}
public interface IGamepadAction : IInputEvent
{
    Windows.Gaming.Input.
    GamepadButtons Buttons { get; }
    byte LeftTrigger { get; }
    byte RightTrigger { get; }
    short LeftThumbstickX { get; }
    short LeftThumbstickY { get; }
    short RightThumbstickX { get; }
    short RightThumbstickY { get; }
}
public interface IPenAction : IInputEvent
{
    Windows.UI.Input.Preview.Injection.
    InjectedInputPointerOptions Options { get; }
    Windows.UI.Input.Preview.Injection.
    InjectedInputPoint Point { get; }
    int Pressure { get; }
}
public interface ITouchAction : IInputEvent
{
    Windows.UI.Input.Preview.Injection.
    InjectedInputPointerOptions Options { get; }
    Windows.UI.Input.Preview.Injection.
    InjectedInputPoint Point { get; }
}
#endif