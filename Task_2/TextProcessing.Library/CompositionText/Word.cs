using System.Collections.Generic;
using System.Linq;

namespace TextProcessing.Library.CompositionText
{
    public class Word
    {
        private Symbol[] _symbols;

        public Word(IEnumerable<Symbol> source)
        {
            _symbols = source.ToArray();
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
       
    }
}