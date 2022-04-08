using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Services
{
    internal class FileService
    {
        public static readonly string outputFilePath = "..\\output.txt";
        public static readonly string outputFolderPath = "..\\outputFiles\\";

        public string GetStringFromFile(string filePath)
        {
            string result = "";
            using (FileStream fs = File.OpenRead(filePath))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    result += temp.GetString(b);
                }
            }
            return result;
        }

        public byte[] GetBinaryData(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public void SaveStringToOutputFile(string output)
        {
            File.WriteAllText(outputFilePath, output);
        }

        public string SaveBytesToFile(string filePath, byte[] bytes)
        {
            System.IO.Directory.CreateDirectory(outputFolderPath);

            var filePathSplitted = filePath.Split('\\');

            var fileName = filePathSplitted[filePathSplitted.Length - 1];

            if (fileName.EndsWith(".bin")) fileName = fileName.Replace(".bin", "");
            else fileName = fileName + ".bin";
            
            File.WriteAllBytes(outputFolderPath + fileName, bytes);

            return outputFolderPath + fileName;
        }
    }
}
