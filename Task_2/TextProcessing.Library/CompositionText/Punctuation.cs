namespace TextProcessing.Library.CompositionText
{
    public class Punctuation
    {
        private Symbol _symbol;

        public Punctuation(Symbol symbol)
        {
            _symbol = symbol;
        }

        public string Chars
        {
            get
            {
                return _symbol.Chars;
            }
        }
    }
}