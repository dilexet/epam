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
                yield return sentence.Value;
            }
        }

        public IEnumerable<Sentence> SortByTheNumberOfWordsInASentence()
        {
            return _sentences.OrderBy(item => item.WordsCount).ToList();
        }

        private IEnumerable<Sentence> ReceivingInterrogativeSentences()
        {
            return _sentences.Where(sentence => sentence.Value.EndsWith("?")).ToList();
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

        // TODO: при удалении слова смещаются, а знаки препиания находятся на прежней позиции, исправить это
        public IEnumerable<Sentence> DeletingWordsOfGivenLengthBeginningWithConsonant(uint lenght)
        {
            var itemSentences = _sentences;
            
            foreach (var sentence in itemSentences)
            {
                sentence.Words.RemoveAll(item => item.IsWordBeginWithConsonant && item.SymbolCount == lenght);
            }
            
            return itemSentences;
        }

        public IEnumerable<Sentence> ReplacingStringWithSubstring(uint lenght, string substring)
        {
            var itemSentences = _sentences;
            foreach (var sentence in itemSentences)
            {
                for (int i = 0; i < sentence.Words.Count; i++)
                {
                    if (sentence.Words[i].SymbolCount == lenght)
                        sentence.Words[i] = new Word(substring);
                }
            }

            return itemSentences;
        }
    }
}