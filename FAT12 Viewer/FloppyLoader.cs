using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FAT12Viewer
{
    public class FloppyLoader
    {
        public byte[] Load(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
