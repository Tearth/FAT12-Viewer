using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FAT12Viewer;

namespace FAT12Viewer
{
    public static class FloppyHeaderParser
    {
        public static FloppyHeader Parse(byte[] floppyData)
        {
            var header = new FloppyHeader();

            var size = Marshal.SizeOf(header);
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(floppyData, 0, ptr, size);

            header = (FloppyHeader)Marshal.PtrToStructure(ptr, header.GetType());
            Marshal.FreeHGlobal(ptr);

            return header;
        }
    }
}
