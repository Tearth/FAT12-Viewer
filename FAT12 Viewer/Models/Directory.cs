using System.Runtime.InteropServices;

namespace FAT12Viewer.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Directory
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] ShortFileName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ShortFileExtension;

        public byte FileAttributes;
        public byte UserAttributes;
        public byte FirstCharacter;
        public ushort CreateTime;
        public ushort CreateDate;
        public ushort LastAccessDate;
        public ushort AccessRights;
        public ushort LastModifiedTime;
        public ushort LastModifiedDate;
        public ushort StartCluster;
        public ushort FileSize;

        public ushort Unused;
    }
}
