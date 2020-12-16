using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TextProcessing.Library.CompositionText
{
    public class Parser
    {

        public Text Parse(StreamReader streamReader)
        {
            string allText = RemoveExtraSymbol(streamReader.ReadToEnd());
            
            Text text = new Text(ParseSentence(allText));
            
            return text;
        }

        private IEnumerable<Sentence> ParseSentence(string source)
        {
            ICollection<Sentence> sentencesList = new List<Sentence>();
            
            string[] sentences = Regex.Split(source, @"(?<=[\.!\?\?!\...])\s+");

            foreach (var sentence in sentences)
            {
                if (!string.IsNullOrEmpty(sentence))
                    sentencesList.Add(new Sentence(ParseWord(sentence), ParsePunctuation(sentence)));
            }

            return sentencesList;
        }

        private List<Word> ParseWord(string sentence)
        {
            List<Word> wordsList = new List<Word>();
            string[] words = Regex.Split(sentence, @"[^а-яА-яa-zA-z0-9]");
            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                    wordsList.Add(new Word(word));
            }

            return wordsList;
        }
        private List<Punctuation> ParsePunctuation(string sentence)
        {
            List<Punctuation> punctuationsList = new List<Punctuation>();
            string buffer = "";
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == ' ' || 
                    sentence[i] == '\n' ||
                    sentence[i] == '\t' ||
                    sentence[i] == '.' || 
                    sentence[i] == ',' || 
                    sentence[i] == '?' || 
                    sentence[i] == '!' || 
                    sentence[i] == ':' || 
                    sentence[i] == '-' || 
                    sentence[i] == ';')
                {
                    buffer += sentence[i];
                }
                else if(!string.IsNullOrEmpty(buffer))
                {
                    punctuationsList.Add(new Punctuation(new Symbol(buffer)));
                    buffer = "";
                }
            }

            if (!string.IsNullOrEmpty(buffer))
                punctuationsList.Add(new Punctuation(new Symbol(buffer)));
            
            return punctuationsList;
        }
        private string RemoveExtraSymbol(string currentString)
        {
            if (currentString == null)
                return null;
            currentString = Regex.Replace(currentString, "[ ]+", " ");
            currentString = Regex.Replace(currentString, "[\t]+", " ");
            currentString = Regex.Replace(currentString, @"(?:\n|\r|\r\n){2,}", "\n");
            return currentString;
        }
    }
}