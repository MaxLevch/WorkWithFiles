using System;
using System.IO;
namespace Task3
{
    class program
    {
        static void Main(string[] args)
        {
            DirSize Direct = new DirSize("C://Users/MaxiL/Desktop/NewFolder");
            long result1 = Direct.CalculSize();
            Console.WriteLine($"Исходный размер папки:{result1} байт");
            DeleteFolderFile DFolder = new DeleteFolderFile("C://Users/MaxiL/Desktop/NewFolder");
            DFolder.DeleteFile();
            long result2 = Direct.CalculSize();
            Console.WriteLine($"Освобождено:{result1 - result2} байт");
            Console.WriteLine($"Текущий размер папки:{result2} байт");
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
        public long CalculSize()
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
    class DeleteFolderFile
    {
        public string pathFolder;
        public DeleteFolderFile(string PathFolder)
        {
            pathFolder = PathFolder;
        }
        public void DeleteFile()
        {
            try
            {
                if (Directory.Exists(pathFolder))
                {
                    string[] dirs = Directory.GetDirectories(pathFolder);
                    foreach (string d in dirs)
                    {
                        DeleteFolderFile deleteFolderFile = new DeleteFolderFile(d);
                        deleteFolderFile.DeleteFile();
                    }

                    string[] files = Directory.GetFiles(pathFolder);
                    foreach (string f in files)
                    {
                        FileInfo file = new FileInfo(f);
                        TimeSpan TimeDelete = new TimeSpan(0, 4, 1);
                        DateTime UseFile = file.LastAccessTime;
                        TimeSpan UseTime = DateTime.Now - UseFile;
                        if (TimeSpan.Compare(TimeDelete, UseTime) <= 0)
                        {
                            file.Delete();
                        }
                    }
                    DirectoryInfo dirInfo = new DirectoryInfo(pathFolder);
                    dirInfo.Delete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }
    }
}