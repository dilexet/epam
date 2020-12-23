using System.Collections.Generic;
using System.Linq;

namespace TextModel.TextElements.SentenceElements
{
    public class SeparatorHelper
    {
        private readonly string[] _wordSeparators = {",", ";", "\"", "'", "\r", "\n", " ", "—", ":"};
        private readonly string[] _sentenceSeparators = {".", "!", "?", "?!", "!?", "..."};
        public bool IsSentenceSeparators(Symbol separator)
        {
            return _sentenceSeparators.Contains(separator.Chars);
        }
        public bool IsWordSeparators(Symbol separator)
        {
            return _wordSeparators.Contains(separator.Chars);
        }
    }
}