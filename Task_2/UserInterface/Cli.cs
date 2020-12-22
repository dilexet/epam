using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using TextConcordance.Library;
using TextModel.Library;
using TextTools.Library.tools;

namespace UserInterface
{
    public class Cli
    {
        private readonly string[] _args;
        private readonly int _lenght;
        private readonly SortedDictionary<string, string> _appSettings;
        private readonly ITextStreamReader _textStreamReader;
        private readonly string _pathRead;
        private readonly string _pathWrite;
        private Text _text;
        public Cli(string[] args)
        {
            _args = args;
            _lenght = _args.Length;
            _pathRead = ConfigurationManager.AppSettings.Get("readFile");
            _pathWrite = ConfigurationManager.AppSettings.Get("writeFile");
            _textStreamReader = new TextStream();
            
            var item = ConfigurationManager.AppSettings;
            _appSettings = new SortedDictionary<string, string>();
            
            for (int i = 0; i < item.Count; i++)
            {
                if (item.GetKey(i).Contains("--"))
                {
                    _appSettings.Add(item.GetKey(i), item[i]);
                }
            }
        }

        public void Run()
        {
            if (!_appSettings.ContainsKey(_args[0]) || _args == null)
            {
                throw new Exception("Error: command does not exist");
            }
            if (_lenght == 1 && _args[0] == "--help")
            {
                Help();
                return;
            }

            if (_lenght == 2 && _args[0] == "--help")
            {
                MethodHelp();
                return;
            }
            
            _text = _textStreamReader.TextReader(_pathRead);
            
            int bufferLenght = 10000;
            StringBuilder stringBuilder = new StringBuilder(bufferLenght);
            stringBuilder.Clear();
            switch (_args[0])
            {
                case "--SortByWordCount":
                    foreach (var item in _text.SortByWordCount())
                    {
                        stringBuilder.Append(item);
                    }
                    break;
                case "--GetWordsGivenLength":
                    if (_lenght != 2 || !uint.TryParse(_args[1], out uint length1))
                    {
                        throw new Exception();
                    }
                    foreach (var item in _text.GetWordsGivenLength(length1))
                    {
                        stringBuilder.Append(item.Value + "\r\n");
                    }
                    break;
                case "--DeleteWordsBeginConsonant":
                    if (_lenght != 2 || !uint.TryParse(_args[1], out uint length2))
                    {
                        throw new Exception();
                    }
                    foreach (var item in _text.DeleteWordsBeginConsonant(length2))
                    {
                        stringBuilder.Append(item);
                    }
                    break;
                case "--ReplaceStringWithSubstring":
                    if (_lenght != 3 || !uint.TryParse(_args[1], out uint length3))
                    {
                        throw new Exception();
                    }
                    foreach (var item in _text.ReplaceStringWithSubstring(length3, _args[2]))
                    {
                        stringBuilder.Append(item);
                    }
                    break;
                case "--GetConcordance":
                    if (_lenght != 2 || !uint.TryParse(_args[1], out uint numberOfLinesPerPage))
                    {
                        throw new Exception();
                    }
                    Concordance concordance = new Concordance(_text, numberOfLinesPerPage);
                    stringBuilder.Append(concordance.GetConcordance());
                    break;
                default:
                    throw new Exception();
            }
            StreamWrite(_pathWrite, stringBuilder.ToString());
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
        private void Help()
        {
            Console.WriteLine("\nНапишите \"--help <--Название метода>\" чтобы узнать подробности о нём");
            foreach (var appSetting  in _appSettings)
            {
                Console.WriteLine($"\n{appSetting.Key}\t{appSetting.Value}");
            }
        }

        private void MethodHelp()
        {
            switch (_args[1])
            {
                case "--SortByWordCount":
                    Console.WriteLine($"\n{_args[1]}");
                    break;
                case "--GetWordsGivenLength":
                    Console.WriteLine($"\n{_args[1]} <length>");
                    break;
                case "--DeleteWordsBeginConsonant":
                    Console.WriteLine($"\n{_args[1]} <length>");
                    break;
                case "--ReplaceStringWithSubstring":
                    Console.WriteLine($"\n{_args[1]} <length> <substring>");
                    break;
                case "--GetConcordance":
                    Console.WriteLine($"\n{_args[1]} <numberOfLinesPerPage>");
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}