using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Keyboard utility class for handling keyboard input and actions.
/// </summary>
public static class Keyboard
{
    public const VirtualKeyModifiers AllModifierKeys = VirtualKeyModifiers.None |
                 VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift |
                 VirtualKeyModifiers.Menu | VirtualKeyModifiers.Windows;
    /// <summary>
    /// Gets the current state of modifier keys.
    /// </summary>
    public static VirtualKeyModifiers GetModifiersState()
    {
        var state = VirtualKeyModifiers.None;
        if (IsDown(VirtualKey.Control))
            state |= VirtualKeyModifiers.Control;
        if (IsDown(VirtualKey.Shift))
            state |= VirtualKeyModifiers.Shift;
        if (IsDown(VirtualKey.Menu))
            state |= VirtualKeyModifiers.Menu;
        if (IsDown(VirtualKey.LeftWindows) ||
            IsDown(VirtualKey.RightWindows))
            state |= VirtualKeyModifiers.Windows;
        return state;
    }
    /// <summary>
    /// Gets a string representation of the currently pressed modifier keys.
    /// </summary>
    public static string GetModifiersString(VirtualKeyModifiers? modifiers = null, bool acceptNone = true)
    {
        var mods = string.Empty;
        modifiers ??= GetModifiersState();
        foreach (var value in Enum.GetValues<VirtualKeyModifiers>())
            if ((value is VirtualKeyModifiers.None && acceptNone && modifiers.Value is VirtualKeyModifiers.None) ||
                (value is not VirtualKeyModifiers.None && modifiers.Value.HasFlag(value)))
                mods += value + "+";
        return mods.TrimEnd('+');
    }
    /// <summary>
    /// Checks if the specified modifier keys are currently pressed with VirtualKeyModifiers input.
    /// </summary>
    public static bool CheckModifiers(VirtualKeyModifiers modifiers) => CheckModifiers(GetModifiersString(modifiers));
    /// <summary>
    /// Checks if the specified modifier keys are currently pressed with string input.
    /// </summary>
    public static bool CheckModifiers(string modifiers)
    {
        var current = GetModifiersState();
        if (modifiers.Contains('-'))        // Or
        {
            var parts = modifiers.Split('-', StringSplitOptions.RemoveEmptyEntries);
            foreach (var group in parts.Where(p => p is not "None").ToList())
            {
                var mask = group.Contains('+')
                    ? ToMaskList(group.Split('+', StringSplitOptions.RemoveEmptyEntries))
                    : ToMask(group);
                if ((current & mask) == mask) return true;
            }
            return parts.Contains("None") && current is VirtualKeyModifiers.None;
        }
        else if (modifiers.Contains('+'))   // And
        {
            var andParts = modifiers.Split('+', StringSplitOptions.RemoveEmptyEntries);
            var mask = ToMaskList(andParts);
            return (current & mask) == mask;
        }
        else if (modifiers is "None")       // None
            return current is VirtualKeyModifiers.None;
        return (current & ToMask(modifiers)) == ToMask(modifiers);
        static VirtualKeyModifiers ToMask(string mod) => mod switch
        {
            "Control" => VirtualKeyModifiers.Control,
            "Shift" => VirtualKeyModifiers.Shift,
            "Menu" => VirtualKeyModifiers.Menu,
            "Windows" => VirtualKeyModifiers.Windows,
            _ => VirtualKeyModifiers.None
        };
        static VirtualKeyModifiers ToMaskList(IEnumerable<string> mods)
        {
            VirtualKeyModifiers mask = 0;
            foreach (var mod in mods)
                mask |= ToMask(mod);
            return mask;
        }
    }
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
    /// Sends a keyboard message sequence using PostMessage API.
    /// </summary>
    /// <returns>Array of results from PostMessage calls.</returns>
    public static bool[] PostMessage(KybdEvent kybd)
    {
        var hWnd = FindHandle(kybd.WindowInfo, false, false);
        var sended = new bool[kybd.Keys.Length];
        for (int i = 0; i < kybd.Keys.Length; i++)
            sended[i] = NativeMethods.PostMessage(hWnd, (uint)kybd.Messages[i],
                (ushort)kybd.Keys[i], nint.Zero);
        return sended;
    }
    /// <summary>
    /// Sends a keyboard message sequence using SendMessage API.
    /// </summary>
    /// <returns>Array of results from SendMessage calls.</returns>
    public static nint[] SendMessage(KybdEvent kybd)
    {
        var hWnd = FindHandle(kybd.WindowInfo, false, false);
        var sended = new nint[kybd.Keys.Length];
        for (int i = 0; i < kybd.Keys.Length; i++)
            sended[i] = NativeMethods.SendMessage(hWnd, (uint)kybd.Messages[i],
                (ushort)kybd.Keys[i], nint.Zero);
        return sended;
    }
    /// <summary>
    /// Keys down event for the specified key using SendInput API.
    /// </summary>
    public static uint[] KeysDown(VirtualKey[] keys, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0)
    {
        var results = new uint[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            results[i] = SendInputs(KybdEvent.Create([keys[i]], [WindowsMessageType.KeyDown],
                useScanCode, useUnicode, isExtendedKey, window, time));
        return results;
    }
    /// <summary>
    /// Keys up event for the specified key using SendInput API.
    /// </summary>
    /// <returns>Array of results from SendInput calls.</returns>
    public static uint[] KeysUp(VirtualKey[] keys, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0)
    {
        var results = new uint[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            results[i] = SendInputs(KybdEvent.Create([keys[i]], [WindowsMessageType.KeyUp],
                useScanCode, useUnicode, isExtendedKey, window, time));
        return results;
    }
    /// <summary>
    /// Sends a keyboard event sequence using SendInput API.
    /// </summary>
    /// <returns>Array of results from SendInput calls.</returns>
    public static uint[] KeyPress(VirtualKey[] keys, bool useScanCode, bool useUnicode,
        bool isExtendedKey, WindowInfo window = null!, int time = 0)
    {
        var results = new uint[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            results[i] = SendInputs(KybdEvent.Create([keys[i]], 
                [WindowsMessageType.KeyDown, WindowsMessageType.KeyUp],
                useScanCode, useUnicode, isExtendedKey, window, time));
        return results;
    }
}