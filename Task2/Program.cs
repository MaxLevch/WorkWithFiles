using System;
using System.IO;
namespace Task2
{
    class program
    {
        static void Main(string[] args)
        {
            DirSize Direct = new DirSize("C://Users/MaxiL/Documents/Wambot");
            long result = Direct.CalculSize();
            Console.WriteLine($"Размер папки:{result} байт");
        }

        
            
    }
    class DirSize
    {
        public long Size;
        DirectoryInfo dir;
        public DirSize(string path)
        {
            dir = new DirectoryInfo(path);
        }
        public  long CalculSize()
        {
            long size = 0;
            try
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    size += f.Length;
                }
                DirectoryInfo[] nextDir = dir.GetDirectories();
                foreach (DirectoryInfo d in nextDir)
                {
                    DirSize dir2 = new DirSize(d.FullName);
                    size += dir2.CalculSize();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return size;
        }

    }
}
