using System.Linq;
using System.Text;
using TextProcessing.Library.Interfaces;

namespace TextProcessing.Library.CompositionText
{
    public class Word : ISentenceItem
    {
        private readonly Symbol[] _symbols;
        public int SymbolCount => _symbols.Length;
        public bool IsWordBeginWithConsonant
        {
            get
            {
                if (_symbols[0].IsConsonant)
                {
                    return true;
                }
                return false;
            }
        }
        public Word(string chars)
        {
            _symbols = chars?.Select(x => new Symbol(x)).ToArray();
        }
        public string Value
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