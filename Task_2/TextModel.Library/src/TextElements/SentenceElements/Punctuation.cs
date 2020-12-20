namespace TextModel.Library.TextElements.SentenceElements
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