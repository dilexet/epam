using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using CommandLine;
using TextConcordance;
using TextModel;
using TextModel.TextElements;
using TextModel.TextElements.SentenceElements;
using TextProcess.UI.Options;
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
            string pathWrite = "";
            string data = CommandLine.Parser.Default.ParseArguments
            <SortByWordCountOptions,
                GetWordsGivenLengthOptions,
                DeleteWordsBeginConsonantOptions,
                ReplaceStringWithSubstringOptions,
                ConcordanceOptions>(_args).
                MapResult(
                    (SortByWordCountOptions options) =>
                {
                    Text text = GetText(options.PathRead, parser);
                    pathWrite = options.PathWrite;
                    return GetStringSentence(text.SortByWordCount());
                },
                (GetWordsGivenLengthOptions options) =>
                {
                    if (options.Lenght <= 0)
                    {
                        throw new ArgumentOutOfRangeException($"Lenght");
                    }
                    Text text = GetText(options.PathRead, parser);
                    pathWrite = options.PathWrite;
                    return GetStringWord(text.GetWordsGivenLength(options.Lenght));
                },
                (DeleteWordsBeginConsonantOptions options) =>
                {
                    if (options.Lenght <= 0)
                    {
                        throw new ArgumentOutOfRangeException($"Lenght");
                    }
                    Text text = GetText(options.PathRead, parser);
                    pathWrite = options.PathWrite;
                    return GetStringSentence(text.DeleteWordsBeginConsonant(options.Lenght));
                },
                (ReplaceStringWithSubstringOptions options) =>
                {
                    if (options.Lenght <= 0)
                    {
                        throw new ArgumentOutOfRangeException($"Lenght");
                    }
                    Text text = GetText(options.PathRead, parser);
                    pathWrite = options.PathWrite;
                    return GetStringSentence(text.ReplaceStringWithSubstring(options.Lenght, options.Substring));
                },
                (ConcordanceOptions options) =>
                {
                    if (options.NumberOfLinesPerPage <= 0)
                    {
                        throw new ArgumentOutOfRangeException($"NumberOfLinesPerPage");
                    }
                    Text text = GetText(options.PathRead, parser);
                    Concordance concordance = new Concordance(
                        text,
                        options.NumberOfLinesPerPage,
                        ConfigurationManager.AppSettings.Get("patternNewLine"));
                    pathWrite = options.PathWrite;
                    return concordance.GetConcordance();
                }, 
                errs => throw new Exception("The method was entered incorrectly"));
            if (string.IsNullOrEmpty(pathWrite))
            {
                pathWrite = ConfigurationManager.AppSettings.Get("writeFile");
            }

            StreamWrite(pathWrite, data);
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

        private Text GetText(string path, IParser parser)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = ConfigurationManager.AppSettings.Get("readFile");
            }

            ITextStreamReader textStreamReader = new TextStream(path);
            Text text = parser.Parse(textStreamReader.TextReader());
            textStreamReader.Dispose();
            return text;
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
                throw new Exception(e.Message);
            }
        }
    }
}