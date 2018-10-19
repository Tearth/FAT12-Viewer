using System;
using System.Collections.Generic;
using System.Text;

namespace FAT12Viewer
{
    public static class FatParser
    {
        public static ushort[] Parse(FloppyHeader floppyHeader, byte[] floppyData, uint index)
        {
            var totalFatSize = floppyHeader.TotalSectors * floppyHeader.BytesPerSector;
            var initialOffset = GetOffsetForFatIndex(floppyHeader, index);
            var fatSize = GetSizeOfFat(floppyHeader);

            var currentOffset = initialOffset;
            var maxOffset = currentOffset + fatSize;

            var clusters = new List<ushort>();

            while (currentOffset < maxOffset)
            {
                var firstByte = floppyData[currentOffset];
                var secondByte = floppyData[currentOffset + 1];
                var thirdByte = floppyData[currentOffset + 2];

                clusters.Add(GetClusterValueWithFullLeft(firstByte, secondByte));
                clusters.Add(GetClusterValueWithFullRight(secondByte, thirdByte));

                currentOffset += 3;
            }

            return clusters.ToArray();
        }

        private static ushort GetOffsetForFatIndex(FloppyHeader floppyHeader, uint index)
        {
            return (ushort)(floppyHeader.BytesPerSector + (index * floppyHeader.BytesPerSector * floppyHeader.SectorsPerFat));
        }

        private static ushort GetSizeOfFat(FloppyHeader floppyHeader)
        {
            return (ushort)(floppyHeader.BytesPerSector * floppyHeader.SectorsPerFat);
        }

        private static ushort GetClusterValueWithFullLeft(byte firstByte, byte secondByte)
        {
            return (ushort)(firstByte | ((secondByte & 0x0F) << 8));
        }

        private static ushort GetClusterValueWithFullRight(byte firstByte, byte secondByte)
        {
            return (ushort)(((firstByte & 0xF0) >> 4) | (secondByte << 4));
        }
    }
}
