using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FAT12Viewer
{
    public static class FloppyLoader
    {
        public static byte[] Load(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
