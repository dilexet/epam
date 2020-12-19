using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var source = Regex.Matches(RemoveExtraSymbol(text), @"(\w+)|([\W_-[\s]]+)|(\s)");
            return new Text(ParseSentence(source));
        }
        private ICollection<Sentence> ParseSentence(MatchCollection source)
        {
            ICollection<Sentence> sentencesList = new List<Sentence>();
            ICollection<ISentenceItem> sentenceItems = new List<ISentenceItem>();
            
            foreach (Match item in source)
            {
                if (IsWord(item.Value))
                {
                    sentenceItems.Add(new Word(item.Value));
                }
                else if (IsWordSeparator(new Symbol(item.Value)))
                {
                    sentenceItems.Add(new Punctuation(new Symbol(item.Value)));
                }
                else if (IsSentenceSeparator(new Symbol(item.Value)))
                {
                    sentenceItems.Add(new Punctuation(new Symbol(item.Value)));
                    sentencesList.Add(new Sentence(sentenceItems));
                    sentenceItems.Clear();
                }
            }
            return sentencesList;
        }
        private bool IsWord(string word)
        {
            return Regex.IsMatch(word, @"(\w+)");
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
            currentString = Regex.Replace(currentString, "[ ]|[\t]+", " ");
            currentString = Regex.Replace(currentString, @"(?:\n|\r|\r\n){2,}", "\n");
            return currentString;
        }
    }
}
