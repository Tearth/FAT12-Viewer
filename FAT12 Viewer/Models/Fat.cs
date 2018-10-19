using System.Runtime.InteropServices;

namespace FAT12Viewer.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Fat
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Directory[] Directories;
    }
}
