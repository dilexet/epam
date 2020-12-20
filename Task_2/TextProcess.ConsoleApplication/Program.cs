using System;
using System.Configuration;
using TextConcordance.Library;
using TextModel.Library;
using TextTools.Library.tools;

namespace TextProcess.ConsoleApplication
{
    internal class Program
    {
        public static void Main()
        {
            string readPath = ConfigurationManager.AppSettings.Get("readFile");

            ITextStreamReader textStreamReader = new TextStream();
            Text text = textStreamReader.TextReader(readPath, new Parser());

            Concordance concordance = new Concordance(text, 1);
            Console.WriteLine(concordance.GetConcordance());
            
            
        }
    }
}