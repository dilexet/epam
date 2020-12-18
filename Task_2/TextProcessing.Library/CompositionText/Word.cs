using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing.Library.CompositionText
{
    public class Word
    {
        private readonly Symbol[] _symbols;

        public int SymbolCount
        {
            get
            {
                return _symbols.Length;
            }
        }

        public Word(string chars)
        {
            if (chars != null)
            {
                _symbols = chars.Select(x => new Symbol(x)).ToArray();
            }
            else
            {
                _symbols = null;
            }
        }

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var symbol in _symbols)
                {
                    sb.Append(symbol.Chars);
                }
                return sb.ToString(); 
            }
        }

    }
}