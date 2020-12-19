using TextProcessing.Library.Interfaces;

namespace TextProcessing.Library.CompositionText
{
    public class Punctuation : ISentenceItem
    {
        private readonly Symbol _symbol;
        public Punctuation(Symbol symbol)
        {
            _symbol = symbol;
        }
        public string Value => _symbol.Chars;
    }
}