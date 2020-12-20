using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextProcessing.Library.CompositionText;
using TextProcessing.Library.Interfaces;

namespace TextProcessing.Library
{
    public class Parser : IParser
    {
        public Text Parse(FileStream fileStream)
        {
            var array = new byte[fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            string text = System.Text.Encoding.Default.GetString(array);
            
            if(string.IsNullOrEmpty(text))
                throw new NullReferenceException("File is empty");
            
            
            return new Text(ParseSentence(text));
        }
        private ICollection<Sentence> ParseSentence(string text)
        {
            ICollection<Sentence> sentencesList = new List<Sentence>();
            ICollection<ISentenceItem> sentenceItems = new List<ISentenceItem>();
            
            SeparatorContainer separatorContainer = new SeparatorContainer();
            
            int bufferlength = 10000;
            StringBuilder buffer = new StringBuilder(bufferlength);
            StringBuilder bufferSentenceSeparator = new StringBuilder(bufferlength);
            StringBuilder bufferWordSeparator = new StringBuilder(bufferlength);
            buffer.Clear();
            bufferSentenceSeparator.Clear();
            var separatorSentence = separatorContainer.SentenceSeparators().ToArray();
            
            foreach (var symbol in text)
            {
                if (IsLetter(new Symbol(symbol)))
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
            return sentencesList;
        }
        private bool IsWord(string word)
        {
            return Regex.IsMatch(word, @"(\w+)"); // как работает?
        }

        private bool IsLetter(Symbol letter)
        {
            return Regex.IsMatch(letter.Chars, @"\w");
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
    }
}
