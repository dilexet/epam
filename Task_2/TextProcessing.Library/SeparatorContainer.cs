using System.Collections.Generic;
using System.Linq;

namespace TextProcessing.Library
{
    public class SeparatorContainer
    {
        private readonly string[] _wordSeparators = {",", ";", "\"", "'", "\r", "\n", " ", "—", ":"};
        private readonly string[] _sentenceSeparators = {".", "!", "?", "?!", "!?", "..."};
        public IEnumerable<string> SentenceSeparators()
        {
            return _sentenceSeparators.AsEnumerable();
        }
        public IEnumerable<string> WordSeparators()
        {
            return _wordSeparators.AsEnumerable();
        }
    }
}