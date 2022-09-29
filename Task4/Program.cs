using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace FinalTask
{
    class program
    {
        static void Main(string[] args)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // десериализация
            using (var fs = new FileStream("C://Users/MaxiL/Desktop/Students.dat", FileMode.Open))
            {
                Student[] Students = (Student[])formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");

                // Создаем папку на рабочем столе
                DirectoryInfo dirInfo = new DirectoryInfo("C://Users/MaxiL/Desktop/Students");
                if (!dirInfo.Exists)
                    dirInfo.Create();
                for (int i = 0; i < Students.Length; i++)
                {
                    Console.WriteLine($"Имя: {Students[i].Name} ---  {Students[i].Group} ---- {Students[i].DateOfBith}"); // просто для сверки, что все записались
                    string path = Path.Combine(dirInfo.FullName, $"{Students[i].Group}.txt");
                    var fileInfo = new FileInfo(path);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        sw.WriteLine($"{Students[i].Name}, {Students[i].DateOfBith}");
                    }
                }
            }
            Console.ReadLine();

        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBith { get; set; }

        public Student(string name, string group, DateTime dateofbith)
        {
            Name = name;
            Group = group;
            DateOfBith = dateofbith;
        }
    }

}