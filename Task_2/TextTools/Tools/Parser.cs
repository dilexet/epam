using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TextModel;
using TextModel.TextElements;
using TextModel.TextElements.SentenceElements;

namespace TextTools.tools
{
    public class Parser : IParser
    {
        private readonly string _patternIsLetter;
        private readonly string _patternRemoveExtraTab;
        private readonly string _patternRemoveExtraNewLine;
        public Parser(string patternIsLetter, string patternRemoveExtraTab, string patternRemoveExtraNewLine)
        {
            _patternIsLetter = patternIsLetter;
            _patternRemoveExtraTab = patternRemoveExtraTab;
            _patternRemoveExtraNewLine = patternRemoveExtraNewLine;
        }
        public Text Parse(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            string text = streamReader.ReadToEnd();

            if (string.IsNullOrEmpty(text))
            {
                throw new NullReferenceException("File is empty");
            }

            return new Text(ParseSentence(RemoveExtraSymbol(text)));
        }
        private ICollection<Sentence> ParseSentence(string text)
        {
            ICollection<Sentence> sentencesList = new List<Sentence>();
            ICollection<ISentenceItem> sentenceItems = new List<ISentenceItem>();
            SeparatorHelper separatorHelper = new SeparatorHelper();
            int bufferlength = 10000;
            StringBuilder buffer = new StringBuilder(bufferlength);
            StringBuilder bufferSentenceSeparator = new StringBuilder(bufferlength);

            buffer.Clear();
            bufferSentenceSeparator.Clear();
            
            foreach (var symbol in text)
            {
                if (IsWordSymbol(new Symbol(symbol)))
                {
                    if (!string.IsNullOrEmpty(bufferSentenceSeparator.ToString()))
                    {
                        sentenceItems.Add(new Punctuation(new Symbol(bufferSentenceSeparator.ToString())));
                        sentencesList.Add(new Sentence(sentenceItems));
                        sentenceItems.Clear();
                        bufferSentenceSeparator.Clear();
                    }
                    buffer.Append(symbol);
                }
                
                else if (separatorHelper.IsWordSeparators(new Symbol(symbol))) 
                {
                    if (!string.IsNullOrEmpty(bufferSentenceSeparator.ToString()))
                    {
                        sentenceItems.Add(new Punctuation(new Symbol(bufferSentenceSeparator.ToString())));
                        sentencesList.Add(new Sentence(sentenceItems));
                        sentenceItems.Clear();
                        bufferSentenceSeparator.Clear();
                    }
                    if (!string.IsNullOrEmpty(buffer.ToString()))
                    {
                        sentenceItems.Add(new Word(buffer.ToString()));
                        buffer.Clear();
                    } 
                    sentenceItems.Add(new Punctuation(new Symbol(symbol)));
                }
                else if(separatorHelper.IsSentenceSeparators(new Symbol(symbol)))
                {
                    bufferSentenceSeparator.Append(symbol);
                    if (!string.IsNullOrEmpty(buffer.ToString()))
                    {
                        sentenceItems.Add(new Word(buffer.ToString()));
                        buffer.Clear();
                    }
                }
            }

            if (!string.IsNullOrEmpty(bufferSentenceSeparator.ToString()))
            {
                sentenceItems.Add(new Punctuation(new Symbol(bufferSentenceSeparator.ToString())));
                sentencesList.Add(new Sentence(sentenceItems));
            }
            return sentencesList;
        }
        private bool IsWordSymbol(Symbol symbol)
        {
            return Regex.IsMatch(
                       symbol.Chars, 
                       _patternIsLetter) ||
                   Regex.IsMatch(
                       symbol.Chars, 
                       "-");
        }
        private string RemoveExtraSymbol(string currentString)
        {
            if (string.IsNullOrEmpty(currentString))
                throw new ArgumentNullException();
            currentString = Regex.Replace(
                currentString, 
                _patternRemoveExtraTab, 
                " ");
            currentString = Regex.Replace(
                currentString,
                _patternRemoveExtraNewLine,
                "\r\n");
            return currentString;
        }
    }
}
