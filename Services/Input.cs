using System.Runtime.InteropServices;
using Windows.System;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Input utility class for handling keyboard, mouse, and hardware input.
/// </summary>
public static class Input
{
    /// <summary>
    /// Sends an array of input events (keyboard, mouse, hardware) to the system.
    /// </summary>
    public static uint SendInputs(IReadOnlyList<INPUT> inputs) =>
        SendInput((uint)inputs.Count, [.. inputs], Marshal.SizeOf<INPUT>());
    /// <summary>
    /// Gets the extra message information for the current thread.
    /// </summary>
    public static nint GetMsgExtraInfo() => GetMessageExtraInfo();
    /// <summary>
    /// Gets whether the specified key is currently pressed down.
    /// </summary>
    public static bool IsKeyDown(VirtualKey key) => GetKeyState((int)key) < 0;
    /// <summary>
    /// Gets whether the specified key is currently released.
    /// </summary>
    public static bool IsKeyUp(VirtualKey key) => !IsKeyDown(key);
    /// <summary>
    /// Gets whether the specified key is currently pressed down at the hardware level.
    /// </summary>
    public static bool IsHardwareKeyDown(VirtualKey key) => GetAsyncKeyState((ushort)key) < 0;
    /// <summary>
    /// Gets whether the specified key is currently released at the hardware level.
    /// </summary>
    public static bool IsHardwareKeyUp(VirtualKey key) => !IsHardwareKeyDown(key);
    /// <summary>
    /// Gets whether the specified toggling key (Caps Lock, Num Lock...) is currently in effect.
    /// </summary>
    public static bool IsTogglingKeyInEffect(VirtualKey key) => (GetKeyState((int)key) & 1) is not 0;
}