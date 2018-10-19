using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAT12Viewer.Loaders;
using FAT12Viewer.Models;
using FAT12Viewer.Parsers;

namespace FAT12Viewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var floppyData = FloppyLoader.Load(args[0]);
            var floppyHeader = FloppyHeaderParser.Parse(floppyData);
            var firstFatData = FatParser.Parse(floppyHeader, floppyData, 0);
            var secondFatData = FatParser.Parse(floppyHeader, floppyData, 1);
            var fatDirectory = FatDirectoryTableParser.Parse(floppyHeader, floppyData);
            var firstFileContent = FileContentReader.GetContent(floppyHeader, floppyData, new List<ushort> {2, 3, 4, 5});

            DisplayFloppyHeader(floppyHeader);
            DisplayChains(firstFatData);
            DisplayFat(fatDirectory);

            Console.WriteLine(firstFileContent);
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

            for (int i = 2; i < fat.Length; i++)
            {
                if (clonedFat[i] != 0)
                {
                    DisplaySingleChain(clonedFat, i);
                }
            }
        }

        private static void DisplaySingleChain(ushort[] fat, int index)
        {
            var chain = new List<ushort>();
            chain.Add((ushort)index);

            while (index < fat.Length && fat[index] != 0xFFF)
            {
                chain.Add(fat[index]);

                var newIndexValue = fat[index];
                fat[index] = 0;
                index = newIndexValue;
            }

            Console.WriteLine(string.Join(" -> ", chain));
        }

        private static void DisplayFat(List<Directory> fat)
        {
            Console.WriteLine();

            foreach (var directory in fat.Where(p => p.FileSize != 0))
            {
                Console.WriteLine();

                Console.WriteLine("Filename: " + Encoding.ASCII.GetString(directory.ShortFileName));
                Console.WriteLine("Extension: " + Encoding.ASCII.GetString(directory.ShortFileExtension));

                Console.WriteLine("File attributes: " + directory.FileAttributes);
                Console.WriteLine("User attributes: " + directory.UserAttributes);
                Console.WriteLine("First character: " + directory.FirstCharacter);
                Console.WriteLine("Create time: " + GetTime(directory.CreateTime));
                Console.WriteLine("Create date: " + GetDate(directory.CreateDate));
                Console.WriteLine("Last access date: " + directory.LastAccessDate);
                Console.WriteLine("Access rights: " + directory.AccessRights);
                Console.WriteLine("Last modified time: " + GetTime(directory.LastModifiedTime));
                Console.WriteLine("Last modified date: " + GetDate(directory.LastModifiedDate));
                Console.WriteLine("Start cluster: " + directory.StartCluster);
                Console.WriteLine("File size: " + directory.FileSize);
            }
        }

        private static string GetTime(ushort time)
        {
            var hour = time >> 10;
            var minutes = (time & 0b00000_11111_00000) >> 5;
            var seconds = (time & 0b00000_00000_11111) * 2;

            return hour + ":" + minutes + ":" + seconds;
        }

        private static string GetDate(ushort date)
        {
            var year = 1980 + (date >> 9);
            var month = (date & 0b00000_01111_00000) >> 5;
            var day = date & 0b00000_00000_11111;

            return day + "-" + month + "-" + year;
        }
    }
}
