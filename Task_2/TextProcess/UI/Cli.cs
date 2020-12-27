using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using CommandLine;
using TextConcordance;
using TextModel;
using TextModel.TextElements;
using TextModel.TextElements.SentenceElements;
using TextTools.tools;
using Parser = TextTools.tools.Parser;

namespace TextProcess.UI
{
    public class Cli
    {
        private readonly string[] _args;
        
        public Cli(string[] args)
        {
            _args = args;
        }
        
        public void Run()
        {
            var sAttr = ConfigurationManager.AppSettings;
            
            IParser parser = new Parser(
                sAttr.Get("patternIsLetter"), 
                sAttr.Get("patternRemoveExtraTab"), 
                sAttr.Get("patternRemoveExtraNewLine"));

            string data;
            CommandLine.Parser.Default.ParseArguments<Options>(_args).WithParsed(option =>
            {
                // TODO: удалить при релизе
                Console.WriteLine("_________________TEST_______________START____________");
                if (string.IsNullOrEmpty(option.PathRead))
                {
                    option.PathRead = ConfigurationManager.AppSettings.Get("readFile");
                }
                ITextStreamReader textStreamReader = new TextStream(option.PathRead);
                Text text = parser.Parse(textStreamReader.TextReader());
                textStreamReader.Dispose();
                switch (option.MethodName)
                {
                    case "SortByWordCount":
                        data = GetStringSentence(text.SortByWordCount());
                        break;
                    case "GetWordsGivenLength":
                        data = GetStringWord(text.GetWordsGivenLength(option.Lenght));
                        break;
                    case "DeleteWordsBeginConsonant":
                        data = GetStringSentence(text.DeleteWordsBeginConsonant(option.Lenght));
                        break;
                    case "ReplaceStringWithSubstring":
                        data = GetStringSentence(text.ReplaceStringWithSubstring(option.Lenght, option.Substring));
                        break;
                    case "Concordance":
                        Concordance concordance = new Concordance(
                            text,
                            option.NumberOfLinesPerPage,
                            ConfigurationManager.AppSettings.Get("patternNewLine"));
                        data = concordance.GetConcordance();
                        break;
                    default:
                        throw new Exception("The method was entered incorrectly");
                }
                if (string.IsNullOrEmpty(option.PathWrite))
                {
                    option.PathWrite = ConfigurationManager.AppSettings.Get("writeFile");
                }
                StreamWrite(option.PathWrite, data);
                // TODO: удалить при релизе
                Console.WriteLine("_________________TEST_______________END____________");
            });
        }

        private string GetStringSentence(IEnumerable<Sentence> sentences)
        {
            int bufferLenght = 10000;
            StringBuilder stringBuilder = new StringBuilder(bufferLenght);
            foreach (var item in sentences)
            {
                stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }

        private string GetStringWord(IEnumerable<Word> words)
        {
            int bufferLenght = 10000;
            StringBuilder stringBuilder = new StringBuilder(bufferLenght);
            foreach (var item in words)
            {
                stringBuilder.Append(item.Value + "\r\n");
            }
            return stringBuilder.ToString();
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