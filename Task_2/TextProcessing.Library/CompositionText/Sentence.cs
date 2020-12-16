using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TextProcessing.Library.CompositionText
{
    public class Sentence
    {
        public List<Word> Words { get; }

        public List<Punctuation> Punctuations { get; }

        public int WordsCount
        {
            get
            {
                return Words.Count;
            }
        }

        public Sentence(List<Word> words, List<Punctuation> punctuations)
        {
            Words = words;
            Punctuations = punctuations;
        }

        public string GetSentence()
        {
            StringBuilder sentence = new StringBuilder();
            for (int i = 0; i < Words.Count; i++)
            {
                sentence.Append(Words[i].Chars + Punctuations[i].Chars);
            }

            return sentence.ToString();
        }

    }
}