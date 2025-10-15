namespace ReisProduction.Wincore.System.Form;
public static class ThemeManager
{
    public static bool ChangeTheme(bool darkMode, nint hWnd)
    {
        try { return DwmSetWindowAttribute(hWnd, 20, [darkMode ? 0 : 1], 4) is 0; }
        catch { return false; }
    }
}