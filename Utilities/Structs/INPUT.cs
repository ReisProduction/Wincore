using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential)]
public struct INPUT
{
    public int type;
    public INPUTUNION u;
}