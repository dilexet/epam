using System;
using System.Configuration;
using System.IO;
using TextProcessing.Library.CompositionText;

namespace TextProcessing.ConsoleApplication
{
    internal class Program
    {
        public static void Main()
        {
            
            Parser parser = new Parser();
            string path = ConfigurationManager.AppSettings.Get("readFile");
            Text text;
            using (StreamReader streamReader = new StreamReader(path))
            {
                text = parser.Parse(streamReader);
            }

            
        }
    }
}