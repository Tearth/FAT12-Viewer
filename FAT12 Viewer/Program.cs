using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FAT12Viewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var floppyData = FloppyLoader.Load(args[0]);
            var floppyHeader = FloppyHeaderParser.Parse(floppyData);
            var firstFatData = FloppyFatParser.Parse(floppyHeader, floppyData, 0);
            var secondFatData = FloppyFatParser.Parse(floppyHeader, floppyData, 1);

            DisplayFloppyHeader(floppyHeader);
            DisplayChains(firstFatData);

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

            Console.WriteLine();
            Console.WriteLine("Drive number: " + floppyHeader.DriveNumber);
            Console.WriteLine("Flags in Windows NT: " + floppyHeader.FlagsInWindowsNt);
            Console.WriteLine("Signature: " + floppyHeader.Signature);
            Console.WriteLine("Volume ID: " + floppyHeader.VolumeId);
            Console.WriteLine("Volume label: " + Encoding.ASCII.GetString(floppyHeader.VolumeLabel));
            Console.WriteLine("System identifier: " + Encoding.ASCII.GetString(floppyHeader.SystemIdentifier));
            Console.WriteLine("Boot code: " + Encoding.ASCII.GetString(floppyHeader.BootCode));
            Console.WriteLine("Bootloader signature: " + floppyHeader.BootSignature);
        }

        private static void DisplayChains(ushort[] fat)
        {
            Console.WriteLine();
            Console.WriteLine("FAT chains:");

            var clonedFat = (ushort[])fat.Clone();

            for (int i = 0; i < fat.Length; i++)
            {
                if (clonedFat[i] >= 0x002 && clonedFat[i] <= 0xFEF)
                {
                    DisplaySingleChain(clonedFat, i);
                }
            }
        }

        private static void DisplaySingleChain(ushort[] fat, int index)
        {
            var chain = new List<ushort>();

            while (index < fat.Length && fat[index] != 0xFFF)
            {
                chain.Add(fat[index]);

                var newIndexValue = fat[index];
                fat[index] = 0;
                index = newIndexValue;

                index++;
            }

            Console.WriteLine(string.Join(" -> ", chain));
        }
    }
}
