using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Explicit)]
public struct INPUTUNION(MOUSEINPUT mi, KEYBDINPUT ki, HARDWAREINPUT hi)
{
    [FieldOffset(0)]
    public MOUSEINPUT mi = mi;
    [FieldOffset(0)]
    public KEYBDINPUT ki = ki;
    [FieldOffset(0)]
    public HARDWAREINPUT hi = hi;
}