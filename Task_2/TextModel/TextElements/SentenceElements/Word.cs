using System;
using System.Linq;

namespace TextModel.TextElements.SentenceElements
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
            Value = chars;
        }

        public string Value { get; }
        
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
            return Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is Word wordOther)
            {
                
                return String.CompareOrdinal(Value, wordOther.Value);
            }
            throw new ArgumentNullException();
        }

       
    }
}