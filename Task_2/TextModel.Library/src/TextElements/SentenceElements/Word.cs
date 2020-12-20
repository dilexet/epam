using System;
using System.Linq;
using System.Text;

namespace TextModel.Library.TextElements.SentenceElements
{
    public class Word : ISentenceItem, IComparable
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

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            return Equals(obj as Word);
        }
        private bool Equals(Word other)
        {
            if (other == null) 
            {
                return false;
            }
            return Equals(Value.ToLower(), other.Value.ToLower());
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public int CompareTo(object obj)
        {
            Word wordOther = obj as Word;
            if (wordOther != null)
            {
                return String.Compare(Value, wordOther.Value, StringComparison.Ordinal);
            }
            throw new ArgumentNullException();
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