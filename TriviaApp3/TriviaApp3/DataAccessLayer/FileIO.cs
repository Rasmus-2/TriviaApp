using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.DataAccessLayer
{
    internal class FileIO
    {
        private static string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string path = strExeFilePath + "../../../../../../../Resources/Files/";
        private static string fileName = "LoggedIn.txt";

        public static List<string> ReadAll()
        {
            List<string> fileContent = new List<string>();
            if (File.Exists(path + fileName))
            {
                using (StreamReader reader = new StreamReader(path + fileName))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        fileContent.Add(line);
                        line = reader.ReadLine();
                    }
                }
            }
            return fileContent;
        }

        public static void DeleteLogin()
        {
            if (File.Exists(path + fileName))
            {
                File.Delete(path + fileName);
            }
        }

        public static void WriteAndReplace(string text)
        {
            using (StreamWriter writer = new StreamWriter(path + fileName))
            {
                writer.WriteLine(text);
            }
        }
    }
}
