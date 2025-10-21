using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
namespace ReisProduction.Wincore.System.Form;
public static class ThemeManager
{
    public static bool ChangeTheme(bool darkMode, nint hWnd)
    {
        try { return DwmSetWindowAttribute(hWnd, 20, [darkMode ? 1 : 0], 4) is 0; }
        catch { return false; }
    }
    public static Color GetAccentColor()
    {
        var set = GetImmersiveUserColorSetPreference(false, false);
        uint type = GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground")),
       colorSetEx = GetImmersiveColorFromColorSetEx((uint)set, type, false, 0);
        return ConvertDWordColorToRGB(colorSetEx);
    }
    public static Color GetIdealTextColor(Color bg) => (bg.R * 0.299 + bg.G * 0.587 + bg.B * 0.114) > 186 ? Color.Black : Color.White;
    public static Color ConvertDWordColorToRGB(uint colorSetEx)
    {
        byte r = (byte)(colorSetEx & 0xFF),
             g = (byte)((colorSetEx >> 8) & 0xFF),
             b = (byte)((colorSetEx >> 16) & 0xFF);
        return Color.FromArgb(r, g, b);
    }
    public static bool IsSystemInDarkMode()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            return key?.GetValue("AppsUseLightTheme") is 0;
        }
        catch { return false; }
    }
    public static (Color Accent, bool IsDarkMode) GetCurrentTheme() => (GetAccentColor(), IsSystemInDarkMode());
}