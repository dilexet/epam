using System.Configuration;
using TextTools.tools;
using UserInterface;

namespace TextProcess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sAttr = ConfigurationManager.AppSettings;
            Cli cli = new Cli(
                new []{ "-m", "GetWordsGivenLength"}, 
                new TextStream(new Parser(
                sAttr.Get("patternIsLetter"),
                sAttr.Get("patternRemoveExtraTab"),
                sAttr.Get("patternRemoveExtraNewLine"))));
            cli.Run();
            
        }
    }
}