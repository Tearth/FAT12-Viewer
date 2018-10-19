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

        // Extended data
        public byte DriveNumber;
        public byte FlagsInWindowsNt;
        public byte Signature;
        public uint VolumeId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] VolumeLabel;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] SystemIdentifier;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 448)]
        public byte[] BootCode;

        public ushort BootSignature;
    }
}
