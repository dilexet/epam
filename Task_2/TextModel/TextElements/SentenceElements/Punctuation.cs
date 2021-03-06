﻿namespace TextModel.TextElements.SentenceElements
{
    public class Punctuation : ISentenceItem
    {
        private readonly Symbol _symbol;
        
        public string Value => _symbol.Chars;
        
        public Punctuation(Symbol symbol)
        {
            _symbol = symbol;
        }
    }
}