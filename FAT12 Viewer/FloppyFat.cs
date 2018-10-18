﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FAT12Viewer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class FloppyFat
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public FloppyDirectory[] Directories;
    }
}