using Windows.System;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Keyboard service for checking key states.
/// </summary>
public static class Keyboard
{
    /// <summary>
    /// Gets whether the specified key is currently pressed down.
    /// </summary>
    public static bool IsKeyDown(VirtualKey key) => GetKeyState((ushort)key) < 0;
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
    public static bool IsTogglingKeyInEffect(VirtualKey key) => (GetKeyState((ushort)key) & 0x1) is 0x1;
}