﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextModel.Library;
using TextModel.Library.TextElements;
using TextModel.Library.TextElements.SentenceElements;

namespace TextTools.Library.tools
{
    public class Parser : IParser
    {
        private readonly string _patternIsLetter;
        private readonly string _patternRemoveExtraTab;
        private readonly string _patternRemoveExtraNewLine;
        public Parser()
        {
            var sAttr = ConfigurationManager.AppSettings;
            _patternIsLetter = sAttr.Get("patternIsLetter");
            _patternRemoveExtraTab = sAttr.Get("patternRemoveExtraTab");
            _patternRemoveExtraNewLine = sAttr.Get("patternRemoveExtraNewLine");
        }
        public Text Parse(Stream stream)
        {
            var array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            string text = Encoding.UTF8.GetString(array);

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
                
                else if (IsWordSeparator(new Symbol(symbol))) 
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
                else if(IsSentenceSeparator(new Symbol(symbol)))
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
        private bool IsWordSeparator(Symbol separator)
        {
            SeparatorContainer separatorContainer = new SeparatorContainer();
            return separatorContainer.WordSeparators().Contains(separator.Chars);
        }
        private bool IsSentenceSeparator(Symbol separator)
        {
            SeparatorContainer separatorContainer = new SeparatorContainer();
            return separatorContainer.SentenceSeparators().Contains(separator.Chars);
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
