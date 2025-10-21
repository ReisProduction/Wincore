using ReisProduction.Windelay.Utilities;
using ReisProduction.Windelay.Models;
using System.Runtime.InteropServices;
using Windows.System;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Keyboard utility class for handling keyboard input and actions.
/// </summary>
public static class Keyboard
{
    /// <inheritdoc cref="Input.IsKeyDown(VirtualKey)"/>
    public static bool IsDown(VirtualKey key) => Input.IsKeyDown(key);
    /// <inheritdoc cref="Input.IsKeyUp(VirtualKey)"/>
    public static bool IsUp(VirtualKey key) => !IsDown(key);
    /// <inheritdoc cref="Input.IsHardwareKeyDown(VirtualKey)"/>
    public static bool IsHardwareDown(VirtualKey key) => Input.IsHardwareKeyDown(key);
    /// <inheritdoc cref="Input.IsHardwareKeyUp(VirtualKey)"/>
    public static bool IsHardwareUp(VirtualKey key) => !IsHardwareDown(key);
    /// <summary>
    /// Sends a keyboard input sequence using SendInput API.
    /// </summary>
    public static uint SendInputs(KybdEvent kybd)
    {
        if (kybd.Keys.Length is 0 || kybd.Messages.Length is 0) return 0;
        int total = kybd.Keys.Length * kybd.Messages.Length, idx = 0;
        var inputs = new INPUT[total];
        for (int i = 0; i < kybd.Keys.Length; i++)
        {
            var key = kybd.Keys[i];
            bool useScan = i < kybd.UseScanCode.Length && kybd.UseScanCode[i],
              isExtended = i < kybd.IsExtendedKey.Length && kybd.IsExtendedKey[i],
              useUnicode = i < kybd.UseUnicode.Length && kybd.UseUnicode[i];
            foreach (var msg in kybd.Messages)
                inputs[idx++] = new()
                {
                    type = INPUT_KEYBOARD,
                    u = new()
                    {
                        ki = new()
                        {
                            wVk = (ushort)(useScan || useUnicode ? 0 : key),
                            wScan = (ushort)(useScan || useUnicode ? key : 0),
                            dwExtraInfo = Input.GetMsgExtraInfo(),
                            dwFlags = (msg is WindowsMessageType.KeyUp ? (int)KeyEventType.KeyUp : 0) |
                                      (useScan ? (int)KeyEventType.ScanCode : 0) |
                                      (isExtended ? (int)KeyEventType.ExtendedKey : 0) |
                                      (useUnicode ? (int)KeyEventType.Unicode : 0),
                            time = kybd.Time
                        }
                    }
                };
        }
        BringToFront(FindHandle(kybd.WindowInfo, false, false));
        return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
    }
    /// <summary>
    /// Sends a keyboard event sequence using keybd_event API.
    /// </summary>
    public static void SendEvents(KybdEvent kybd)
    {
        BringToFront(FindHandle(kybd.WindowInfo, false, false));
        for (int i = 0; i < kybd.Keys.Length; i++)
            keybd_event((byte)kybd.Keys[i], 0, (int)kybd.Messages[i], (int)GetMessageExtraInfo());
    }
    /// <summary>
    /// Keys down event for the specified key using SendInput API.
    /// </summary>
    public static uint KeyDown(VirtualKey key, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0) =>
        SendInputs(KybdEvent.Create([key], [WindowsMessageType.KeyDown],
            useScanCode, useUnicode, isExtendedKey, window, time));
    /// <summary>
    /// Keys down event for the specified key using keybd_event API.
    /// </summary>
    public static void KeyDownEvent(VirtualKey key, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0) =>
        SendEvents(KybdEvent.Create([key], [WindowsMessageType.KeyDown],
            useScanCode, useUnicode, isExtendedKey, window, time));
    /// <summary>
    /// Keys up event for the specified key using SendInput API.
    /// </summary>
    public static uint KeyUp(VirtualKey key, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0) =>
        SendInputs(KybdEvent.Create([key], [WindowsMessageType.KeyUp],
            useScanCode, useUnicode, isExtendedKey, window, time));
    /// <summary>
    /// Keys up event for the specified key using keybd_event API.
    /// </summary>
    public static void KeyUpEvent(VirtualKey key, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0) =>
        SendEvents(KybdEvent.Create([key], [WindowsMessageType.KeyUp],
            useScanCode, useUnicode, isExtendedKey, window, time));
    /// <summary>
    /// Keys press (down + up) for the specified key using SendInput API.
    /// </summary>
    public static uint KeyPress(VirtualKey key, DelayAction? delayAction = null, bool useScanCode = false,
        bool useUnicode = false, bool isExtendedKey = false, WindowInfo window = null!)
    {
        uint total;
        if (delayAction is null)
            total = 2 * SendInputs(KybdEvent.Create([key], [WindowsMessageType.KeyDown | WindowsMessageType.KeyUp],
                useScanCode, useUnicode, isExtendedKey, window));
        else
        {
            total = KeyDown(key, useScanCode, useUnicode, isExtendedKey, window);
            DelayExecutor.HandleDelay(delayAction).GetAwaiter().GetResult();
            total += KeyUp(key, useScanCode, useUnicode, isExtendedKey, window);
        }
        return total;
    }
    /// <summary>
    /// Keys press (down + up) for the specified key using keybd_event API.
    /// </summary>
    public static void KeyPressEvent(VirtualKey key, DelayAction? delayAction = null, bool useScanCode = false,
        bool useUnicode = false, bool isExtendedKey = false, WindowInfo window = null!)
    {
        if (delayAction is null)
            SendEvents(KybdEvent.Create([key], [WindowsMessageType.KeyDown | WindowsMessageType.KeyUp],
                useScanCode, useUnicode, isExtendedKey, window));
        else
        {
            KeyDownEvent(key, useScanCode, useUnicode, isExtendedKey, window);
            DelayExecutor.HandleDelay(delayAction).GetAwaiter().GetResult();
            KeyUpEvent(key, useScanCode, useUnicode, isExtendedKey, window);
        }
    }
}