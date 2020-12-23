using System;
using System.Configuration;
using System.IO;
using System.Text;
using CommandLine;
using TextConcordance;
using TextModel;
using TextTools.tools;

namespace UserInterface
{
    public class Cli
    {
        
        [Option(
            'm',
            "methodName",
            Required = false,
            HelpText = "Input method name"
            )]
        public string MethodName { get; set; }
        //
        [Option(
            'l',
            "lenght",
            Required = false,
            HelpText = "Input number of characters in a word (method parameter)",
            Default = 5
        )]
        public int Lenght { get; set; }
        //
        [Option(
            's',
            "substring",
            Required = false,
            HelpText = "Input substring (method parameter)",
            Default = "Hello"
        )]
        public string Substring { get; set; }
        //
        [Option(
            'n',
            "numberOfLinesPerPage",
            Required = false,
            HelpText = "Input the number of lines per page (method parameter)",
            Default = 1
        )]
        public int NumberOfLinesPerPage { get; set; }
        //
        [Option(
            'r',
            "pathRead",
            Required = false,
            HelpText = "Input the file path to read",
            Default = "C:\\Users\\dilexet\\Documents\\epam\\Task_2\\TextProcess\\file_1.txt"
            )]
        public string PathRead { get; set; }
        //
        [Option(
            'w',
            "pathWrite",
            Required = false,
            HelpText = "Input the file path to write",
            Default = "C:\\Users\\dilexet\\Documents\\epam\\Task_2\\TextProcess\\file_2.txt"
            
        )]
        public string PathWrite { get; set; }

        private readonly string[] _args;
        private Text _text;
        private readonly ITextStreamReader _textStreamReader;
        public Cli(string[] args, ITextStreamReader textStreamReader)
        {
            _args = args;
            _textStreamReader = textStreamReader;
        }

        public Cli()
        {
            
        }
        public void Run()
        {
            int bufferLenght = 10000;
            StringBuilder stringBuilder = new StringBuilder(bufferLenght);
            stringBuilder.Clear();
            CommandLine.Parser.Default.ParseArguments<Cli>(_args).WithParsed(option =>
            {
                // TODO: удалить при релизе
                Console.WriteLine("_________________TEST_______________START____________");
                
                _text = _textStreamReader.TextReader(option.PathRead);
                
                // Tasks
                if (option.MethodName == "SortByWordCount")
                {
                    foreach (var item in _text.SortByWordCount())
                    {
                        stringBuilder.Append(item);
                    }
                }
                else if (option.MethodName == "GetWordsGivenLength")
                {
                    foreach (var item in _text.GetWordsGivenLength(option.Lenght))
                    {
                        stringBuilder.Append(item.Value + "\r\n");
                    }
                }
                else if (option.MethodName == "DeleteWordsBeginConsonant")
                {
                    foreach (var item in _text.DeleteWordsBeginConsonant(option.Lenght))
                    {
                        stringBuilder.Append(item);
                    }
                }
                else if (option.MethodName == "ReplaceStringWithSubstring")
                {
                    foreach (var item in _text.ReplaceStringWithSubstring(option.Lenght, option.Substring))
                    {
                        stringBuilder.Append(item);
                    }
                }
                else if (option.MethodName == "Concordance")
                {
                    Concordance concordance = new Concordance(
                        _text,
                        option.NumberOfLinesPerPage,
                        ConfigurationManager.AppSettings.Get("patternNewLine"));
                    stringBuilder.Append(concordance.GetConcordance());
                }
                
                StreamWrite(option.PathWrite, stringBuilder.ToString());
                // TODO: удалить при релизе
                Console.WriteLine("_________________TEST_______________END____________");
            });
        }
        private void StreamWrite(string path, string data)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new NullReferenceException("The file path is incorrect");
            }
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    byte[] array = Encoding.Default.GetBytes(data);
                    fileStream.Write(array, 0, array.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}