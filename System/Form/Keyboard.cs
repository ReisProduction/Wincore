namespace ReisProduction.Wincore.System.Form;
public static class Keyboard
{
    public static void SendKeys(KybdEvent kybd) => SendKeys(string.Concat(kybd.Keys.Select(k => k.ToKeyString())), FindHandle(kybd.WindowInfo));
    public static void SendKeys(string keys, nint hWnd = 0)
    {
        BringToFront(hWnd, false, false);
        global::System.Windows.Forms.SendKeys.Send(keys);
    }
    public static void SendWait(KybdEvent kybd) => SendWait(string.Concat(kybd.Keys.Select(k => k.ToKeyString())), FindHandle(kybd.WindowInfo));
    public static void SendWait(string keys, nint hWnd = 0)
    {
        BringToFront(hWnd, false, false);
        global::System.Windows.Forms.SendKeys.SendWait(keys);
    }
}