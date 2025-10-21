using System.Runtime.InteropServices;
namespace ReisProduction.Wincore.Utilities.Structs;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct INPUT(int type, INPUTUNION u)
{
    public int type = type;
    public INPUTUNION u = u;
}