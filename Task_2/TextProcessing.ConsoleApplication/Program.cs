using System;
using System.Configuration;
using System.IO;
using TextProcessing.Library;
using TextProcessing.Library.CompositionText;
using TextProcessing.Library.Interfaces;

namespace TextProcessing.ConsoleApplication
{
    internal class Program
    {
        public static void Main()
        {
            IParser parser = new Parser();
            string readPath = ConfigurationManager.AppSettings.Get("readFile");
            string writePath = ConfigurationManager.AppSettings.Get("writeFile");
            
            if (string.IsNullOrEmpty(readPath) || string.IsNullOrEmpty(writePath)) 
                throw new NullReferenceException("The file path is incorrect");
            Text text;
            try
            {
                using (FileStream fileStream = File.OpenRead(readPath))
                {
                    text = parser.Parse(fileStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            try
            {
                using (FileStream fileStream = new FileStream(writePath, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text.ToString());
                    fileStream.Write(array, 0, array.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            Console.WriteLine();
            Console.Write(text);

            Console.WriteLine();
            Console.WriteLine();
           
            Console.WriteLine();
        }
    }
}