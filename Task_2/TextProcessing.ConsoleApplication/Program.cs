using System.Configuration;
using TextProcessing.Library;
using TextProcessing.Library.CompositionText;
using TextProcessing.Library.Interfaces;

namespace TextProcessing.ConsoleApplication
{
    internal class Program
    {
        public static void Main()
        {
           
            string readPath = ConfigurationManager.AppSettings.Get("readFile");
            string writePath = ConfigurationManager.AppSettings.Get("writeFile");
            
            
            ITextStreamReader textStreamReader = new TextStream();
            
            Text text = textStreamReader.TextReader(readPath, new Parser());
            
            ITextStreamWriter textStreamWriter = new TextStream();
            
            textStreamWriter.TextWriter(writePath, text);
        }
    }
}