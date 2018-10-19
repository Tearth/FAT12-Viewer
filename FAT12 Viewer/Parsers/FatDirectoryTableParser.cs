using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using FAT12Viewer.Models;

namespace FAT12Viewer.Parsers
{
    public static class FatDirectoryTableParser
    {
        public static List<Directory> Parse(FloppyHeader floppyHeader, byte[] floppyData)
        {
            var directories = new List<Directory>();

            var sectorsToParse = floppyHeader.DirectoryEntries * 32 / floppyHeader.BytesPerSector;
            var initialOffset = (ushort)(floppyHeader.BytesPerSector + (2 * floppyHeader.BytesPerSector * floppyHeader.SectorsPerFat));
            var offset = initialOffset;

            for (var sector = 0; sector < sectorsToParse; sector++)
            {
                var data = floppyData.Skip(offset).Take(512).ToArray();

                var fat = new Fat();
                var size = Marshal.SizeOf(fat);
                var ptr = Marshal.AllocHGlobal(size);

                Marshal.Copy(data, 0, ptr, size);

                fat = (Fat)Marshal.PtrToStructure(ptr, fat.GetType());
                Marshal.FreeHGlobal(ptr);

                directories.AddRange(fat.Directories);
                offset += floppyHeader.BytesPerSector;
            }

            return directories;
        }
    }
}
