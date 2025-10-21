using System.Runtime.InteropServices;
using System.Drawing;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct RECT
{
    public int Left, Top, Right, Bottom;
    public RECT(int left, int top, int right, int bottom) =>
        (Left, Top, Right, Bottom) = (left, top, right, bottom);
    public RECT(int x, int y, int width, int height, bool sizeMode)
    {
        Left = x;
        Top = y;
        Right = sizeMode ? x + width : width;
        Bottom = sizeMode ? y + height : height;
    }
    public readonly int Width => Right - Left;
    public readonly int Height => Bottom - Top;
    public readonly Point Location => new(Left, Top);
    public readonly Size Size => new(Width, Height);
    public readonly Rectangle ToRectangle() => Rectangle.FromLTRB(Left, Top, Right, Bottom);
    public static RECT FromRectangle(Rectangle rect) =>
        new(rect.Left, rect.Top, rect.Right, rect.Bottom);
    public override readonly string ToString() =>
        $"RECT [L={Left}, T={Top}, R={Right}, B={Bottom}, W={Width}, H={Height}]";
}