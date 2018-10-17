using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FAT12Viewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var floppyLoader = new FloppyLoader();
            var floppyHeaderParser = new FloppyHeaderParser();

            var floppyData = floppyLoader.Load(args[0]);
            var floppyHeader = floppyHeaderParser.Parse(floppyData);

            DisplayFloppyHeader(floppyHeader);

            Console.ReadLine();
        }

        private static void DisplayFloppyHeader(FloppyHeader floppyHeader)
        {
            Console.WriteLine("Boot trap: " + Encoding.ASCII.GetString(floppyHeader.BootTrap));
            Console.WriteLine("OEM: " + Encoding.ASCII.GetString(floppyHeader.Oem));
            Console.WriteLine("Bytes per sector: " + floppyHeader.BytesPerSector);
            Console.WriteLine("Sectors per cluster: " + floppyHeader.SectorsPerCluster);
            Console.WriteLine("Reserved sectors: " + floppyHeader.ReservedSectors);
            Console.WriteLine("FAT count: " + floppyHeader.FatCount);
            Console.WriteLine("Directory entries: " + floppyHeader.DirectoryEntries);
            Console.WriteLine("Total sectors: " + floppyHeader.TotalSectors);
            Console.WriteLine("Media descriptor type: " + floppyHeader.MediaDescriptorType);
            Console.WriteLine("Sectors per FAT: " + floppyHeader.SectorsPerFat);
            Console.WriteLine("Sectors per track: " + floppyHeader.SectorsPerTrack);
            Console.WriteLine("Sides count: " + floppyHeader.SidesCount);
            Console.WriteLine("Hidden sectors: " + floppyHeader.HidddenSectors);
            Console.WriteLine("Large amount: " + floppyHeader.LargeAmount);
        }
    }
}
