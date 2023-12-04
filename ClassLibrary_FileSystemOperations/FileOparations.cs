using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_FileSystemOperations
{
    public static class FileOparations
    {
        //метод копирования файлов
        public static void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath, true);
        }
        //метод копирования директорий
        public static void CopyDirectory(string sourceDirPath, string destinationDirPath)
        {
            if (!Directory.Exists(destinationDirPath))
                Directory.CreateDirectory(destinationDirPath);

            string[] files = Directory.GetFiles(sourceDirPath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationFilePath = Path.Combine(destinationDirPath, fileName);
                File.Copy(file, destinationFilePath, true);
            }

            string[] directories = Directory.GetDirectories(sourceDirPath);
            foreach (string directory in directories)
            {
                string dirName = Path.GetFileName(directory);
                string destinationDirectoryPath = Path.Combine(destinationDirPath, dirName);
                CopyDirectory(directory, destinationDirectoryPath);
            }
        }
        //метод удаления файла по имени
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        //метод удаления  файлов по набору имен
        public static void DeleteFiles(string filePath)
        {
                if (File.Exists(filePath))
                    File.Delete(filePath);
        }

        //метод удаления  файлов по маске
        public static void DeleteFilesByMask(string sourceDirPath, string fileMask)
        {
         
            DirectoryInfo dirInfo = new DirectoryInfo(sourceDirPath);
            FileInfo[] files = dirInfo.GetFiles(fileMask);// по сути тоже что и в том методе
           
            foreach (FileInfo file in files)
            {
                    file.Delete();
               
            }
           
        }
        //метод переноса  файла
        public static void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            File.Move(sourceFilePath, destinationFilePath);

        }

        //метод поиска слова  в файле
        public static bool SearchWordInFile(string filePath, string wordToSearch)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();

                if (content.Contains(wordToSearch))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        //метод поиска слова  в папке
        public static List<string> SearchWordInFolder(string folderPath, string searchTerm)
        {
            List<string> matchingFiles = new List<string>();// список строк для хранения путей к файлам где слово найдено
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.txt");//cоздаем массив строк
                                                                            //содержащих пути к файлам txt в папке
                foreach (string file in files) // перебираем их
                {
                    string content = File.ReadAllText(file);//считываем все текстовые данные из каждого файла

                    if (content.Contains(searchTerm))
                    {
                        matchingFiles.Add(file);// добавляем путь в коллекцию

                    }
                }
            }

            return matchingFiles;//возвращаем коллекцию путей к файлам (где есть слово)
        }








    }
}
