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
        public static string outputFilePath = "..\\output.txt";
        
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

        public void SaveStringToOutputFile(string output)
        {
            File.WriteAllText(outputFilePath, output);
        }
    }
}
