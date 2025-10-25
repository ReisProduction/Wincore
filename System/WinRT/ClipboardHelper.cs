using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
namespace ReisProduction.Wincore.System.WinRT;
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
            var content = Clipboard.GetContent();
            if (content.Contains(StandardDataFormats.Text))
                text = content.GetTextAsync().GetAwaiter().GetResult();
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
            DataPackage pkg = new();
            pkg.SetText(text);
            Clipboard.SetContent(pkg);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Returns true if an bitmap is available, otherwise false.
    /// </summary>
    public static bool TryGetClipboardBitmap(out RandomAccessStreamReference? bitmap)
    {
        bitmap = null;
        try
        {
            var content = Clipboard.GetContent();
            if (content.Contains(StandardDataFormats.Bitmap))
                bitmap = content.GetBitmapAsync().GetAwaiter().GetResult();
            return bitmap is not null;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to set a bitmap to the clipboard. 
    /// Returns true if the provided object is valid and successfully stored.
    /// </summary>
    public static bool TrySetClipboardBitmap(RandomAccessStreamReference bitmap)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(bitmap);
            DataPackage pkg = new();
            pkg.SetBitmap(bitmap);
            Clipboard.SetContent(pkg);
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to read the raw clipboard content object. 
    /// Returns true if content is available, otherwise false.
    /// </summary>
    public static bool TryGetClipboardContent(out DataPackageView? content)
    {
        content = null;
        try
        {
            content = Clipboard.GetContent();
            return content is not null;
        }
        catch { return false; }
    }
    /// <summary>
    /// Attempts to set the raw clipboard content object. 
    /// Returns true if the provided object is valid and successfully stored.
    /// </summary>
    public static bool TrySetClipboardContent(DataPackage content)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(content);
            Clipboard.SetContent(content);
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