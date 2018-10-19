using System.IO;

namespace FAT12Viewer.Loaders
{
    public static class FloppyLoader
    {
        public static byte[] Load(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
