using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FAT12Viewer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class FloppyHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] BootTrap;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Oem;

        public ushort BytesPerSector;
        public byte SectorsPerCluster;
        public ushort ReservedSectors;
        public byte FatCount;
        public ushort DirectoryEntries;
        public ushort TotalSectors;
        public byte MediaDescriptorType;
        public ushort SectorsPerFat;
        public ushort SectorsPerTrack;
        public ushort SidesCount;
        public uint HidddenSectors;
        public uint LargeAmount;
    }
}
