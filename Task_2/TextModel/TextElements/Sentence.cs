using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextModel.TextElements.SentenceElements;

namespace TextModel.TextElements
{
    public class Sentence
    {
        private readonly ICollection<ISentenceItem> _sentenceItems;
        
        public int WordsCount => _sentenceItems.OfType<Word>().Count();
        
        public bool IsSentenceInterrogative
        {
            get
            {
                if (_sentenceItems.OfType<Punctuation>().Last().Value == "?")
                {
                    return true;
                }
                return false;
            }
        }
        
        public Sentence(ICollection<ISentenceItem> sentenceItems)
        {
            _sentenceItems = sentenceItems.ToList();
        }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var sentenceItem in _sentenceItems)
            {
                stringBuilder.Append(sentenceItem.Value);
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<ISentenceItem> GetSentenceItems()
        {
            return _sentenceItems;
        }
    }
}