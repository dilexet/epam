using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextProcessing.Library.CompositionText
{
    public class Text
    {
        private ICollection<Sentence> _sentences;

        public Text(ICollection<Sentence> sentences)
        {
            _sentences = sentences;
        }

        public IEnumerable<string> GetEnumerator()
        {
            foreach (var sentence in _sentences)
            {
                yield return sentence.GetSentence();
            }
        }

        public IEnumerable<Sentence> SortByTheNumberOfWordsInASentence()
        {
            return _sentences.OrderBy(item => item.WordsCount).ToList();
        }

        private IEnumerable<Sentence> ReceivingInterrogativeSentences()
        {
            return _sentences.Where(sentence => sentence.GetSentence().EndsWith("?")).ToList();
        }

        private IEnumerable<Word> RemovingDuplicateItems(List<Word> words)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = i + 1; j < words.Count; j++)
                {
                    if (words[i].Chars == words[j].Chars)
                        words.Remove(words[j]);
                }
            }
            return words;
        }
        public IEnumerable<Word> FetchingWordsOfAGivenLength(uint length)
        {
            List<Word> words = new List<Word>();
            foreach (var sentence in ReceivingInterrogativeSentences())
            {
                words = sentence.Words.Where(word => word.SymbolCount == length).ToList();
            }

            return RemovingDuplicateItems(words);
        }
    }
}