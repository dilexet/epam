using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextModel.TextElements;
using TextModel.TextElements.SentenceElements;

namespace TextModel
{
    public class Text
    {
        private readonly IEnumerable<Sentence> _sentences;
        public Text(ICollection<Sentence> sentences)
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
        // Task 1
        public IEnumerable<Sentence> SortByWordCount()
        {
            return _sentences.OrderBy(item => item.WordsCount);
        }
        // Task 2
        public IEnumerable<Word> GetWordsGivenLength(uint length)
        {
            List<Word> words = new List<Word>();
            foreach (var sentence in ReceiveInterrogativeSentences())
            {
                words = sentence.GetSentenceItems().OfType<Word>().Where(item => item.SymbolCount == length).ToList();
            }

            return words.Distinct();
        }
        // Task 3
        public IEnumerable<Sentence> DeleteWordsBeginConsonant(uint lenght)
        {
            var itemSentences = _sentences.ToList();
            
            for (int j = 0; j < itemSentences.Count; j++) 
            {
                var words = itemSentences[j].GetSentenceItems().OfType<Word>()
                    .Where(item => item.SymbolCount == lenght && item.IsWordBeginWithConsonant).ToList();
                for (int i = 0; i < words.Count(); i++)
                {
                    var item = itemSentences[j].GetSentenceItems().ToList();
                    item.Remove(words[i]);
                    itemSentences[j] = new Sentence(item);
                }
            }
            return itemSentences;
        }
        // Task 4
        // TODO: Нужно ли предоставлять пользователю выбор в каком предложении делать замену?!
        public IEnumerable<Sentence> ReplaceStringWithSubstring(uint lenght, string substring)
        {
            var itemSentences = _sentences.ToList();
            for (int j = 0; j < itemSentences.Count; j++)
            {
                for (int i = 0; i < itemSentences[j].WordsCount; i++)
                {
                    var words = itemSentences[j].GetSentenceItems().OfType<Word>()
                        .Where(item => item.SymbolCount == lenght).ToList();

                    foreach (var word in words)
                    {
                        int index = itemSentences[j].GetSentenceItems().ToList().IndexOf(word);
                        var itemSentence = itemSentences[j].GetSentenceItems().ToList();
                        itemSentence[index] = new Word(substring);
                        itemSentences[j] = new Sentence(itemSentence);
                    }
                }
            }

            return itemSentences;
        }
        private IEnumerable<Sentence> ReceiveInterrogativeSentences()
        {
            return _sentences.Where(sentence => sentence.IsSentenceInterrogative);
        }
    }
}