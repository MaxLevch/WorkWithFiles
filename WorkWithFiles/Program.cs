using System;
using System.IO;
namespace Task1
{
    class program
    {
        static void Main(string[] args)
        {
            DeleteFolderFile DFolder = new DeleteFolderFile("C://Users/MaxiL/Desktop/NewFolder");
            DFolder.DeleteFile();
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
                        TimeSpan TimeDelete = new TimeSpan(0, 30, 1);
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