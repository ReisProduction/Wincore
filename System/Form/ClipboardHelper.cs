using System.Windows.Forms;
using System.Drawing;
namespace ReisProduction.Wincore.System.Form;
/// <summary>
/// Clipboard helper for handling clipboard operations.
/// </summary>
public static class ClipboardHelper
{
    /// <summary>
    /// Attempts to read text from the clipboard. 
    /// Returns true if non-empty text is available, otherwise false.
    /// </summary>
    public static bool TryGetClipboardText(out string? text)
    {
        text = null;
        try
        {
            if (Clipboard.ContainsText())
                text = Clipboard.GetText();
            return !string.IsNullOrWhiteSpace(text);
        }
        catch { return false; }
    }
    /// <summary>
    /// Returns true on success, otherwise false.
    /// </summary>
    public static bool TrySetClipboardText(string text)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(text);
            Clipboard.SetText(text);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Returns true if an image is available, otherwise false.
    /// </summary>
    public static bool TryGetClipboardBitmap(out Image? bitmap)
    {
        bitmap = null;
        try
        {
            if (Clipboard.ContainsImage())
                bitmap = Clipboard.GetImage();
            return bitmap is not null;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to set a bitmap to the clipboard. 
    /// Returns true if the provided object is valid and successfully stored.
    /// </summary>
    public static bool TrySetClipboardBitmap(Image bitmap)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(bitmap);
            Clipboard.SetImage(bitmap);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to read the raw clipboard content object. 
    /// Returns true if content is available, otherwise false.
    /// </summary>
    public static bool TryGetClipboardContent(out IDataObject? content)
    {
        content = null;
        try
        {
            content = Clipboard.GetDataObject();
            return content is not null;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to set the raw clipboard content object. 
    /// Returns true if the provided object is valid and successfully stored.
    /// </summary>
    public static bool TrySetClipboardContent(IDataObject content)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(content);
            Clipboard.SetDataObject(content, true);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Tries to clear the clipboard content.
    /// Returns true on success, otherwise false.
    /// </summary>
    public static bool TryClearClipboardContent()
    {
        try
        {
            Clipboard.Clear();
            return true;
        }
        catch { return false; }
    }
}