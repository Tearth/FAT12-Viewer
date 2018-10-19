using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAT12Viewer.Models;

namespace FAT12Viewer.Parsers
{
    public static class FileContentReader
    {
        public static string GetContent(FloppyHeader floppyHeader, byte[] floppyData, List<ushort> chain)
        {
            var content = "";

            foreach (var sector in chain)
            {
                var physicalOffset = GetPhysicalOffset(floppyHeader, sector);
                var data = floppyData.Skip(physicalOffset).Take(512).ToArray();

                content += Encoding.ASCII.GetString(data);
            }

            return content;
        }

        private static ushort GetPhysicalOffset(FloppyHeader floppyHeader, ushort sector)
        {
            var rootDirectory = floppyHeader.DirectoryEntries * 32;
            var initialOffset = (ushort)(floppyHeader.BytesPerSector + 2 * floppyHeader.BytesPerSector * floppyHeader.SectorsPerFat) + rootDirectory;

            return (ushort)(initialOffset + (sector - 2) * 512);
        }
    }
}
