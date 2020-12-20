using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextModel.Library.TextElements;
using TextModel.Library.TextElements.SentenceElements;

namespace TextModel.Library
{
    public class Text
    {
        private readonly IEnumerable<Sentence> _sentences;
        public Text(IEnumerable<Sentence> sentences)
        {
            _sentences = sentences;
        }
        public IEnumerable<Sentence> GetSentence()
        {
            return _sentences;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var sentence in _sentences)
            {
                builder.Append(sentence);
            }
            return builder.ToString();
        }
        public IEnumerable<Sentence> SortByTheNumberOfWordsInASentence()
        {
            return _sentences.OrderBy(item => item.WordsCount);
        }
        public IEnumerable<Word> FetchingWordsOfAGivenLength(uint length)
        {
            List<Word> words = new List<Word>();
            foreach (var sentence in ReceiveInterrogativeSentences())
            {
                words = sentence.GetSentenceItems().OfType<Word>().Where(item => item.SymbolCount == length).ToList();
            }

            return RemovingDuplicateItems(words);
        }
        private IEnumerable<Sentence> ReceiveInterrogativeSentences()
        {
            return _sentences.Where(sentence => sentence.IsSentenceInterrogative);
        }
        private IEnumerable<Word> RemovingDuplicateItems(ICollection<Word> words)
        {
            var wordsList = words.ToList();
            for (int i = 0; i < wordsList.Count; i++)
            {
                for (int j = i + 1; j < wordsList.Count; j++)
                {
                    if (wordsList[i].Value == wordsList[j].Value)
                    {
                        words.Remove(wordsList[j]);
                    }
                }
            }
            return words;
        }
        public IEnumerable<Sentence> DeleteWordsBeginWithConsonant(uint lenght)
        {
            var itemSentences = _sentences.ToList();
            
            foreach (var sentence in itemSentences)
            {
                var words = sentence.GetSentenceItems().OfType<Word>()
                    .Where(item => item.SymbolCount == lenght && item.IsWordBeginWithConsonant).ToList();
                for (int i = 0; i < words.Count(); i++)
                {
                    sentence.GetSentenceItems().ToList().Remove(words[i]);
                }
            }
            return itemSentences;
        }
        public IEnumerable<Sentence> ReplaceStringWithSubstring(uint lenght, string substring)
        {
            var itemSentences = _sentences.ToList();
            foreach (var sentence in itemSentences)
            {
                for (int i = 0; i < sentence.WordsCount; i++)
                {
                    var words = sentence.GetSentenceItems().OfType<Word>()
                        .Where(item => item.SymbolCount == lenght).ToList();

                    foreach (var word in words)
                    {
                        int index = sentence.GetSentenceItems().ToList().IndexOf(word);
                        sentence.GetSentenceItems().ToList()[index] = new Word(substring);
                    }
                }
            }
            return itemSentences;
        }
    }
}