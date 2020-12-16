using System;

namespace TextProcessing.Library.CompositionText
{
    public struct Symbol
    {
        public string Chars { get; }

        public Symbol(string chars)
        {
            Chars = chars;
        }

        public Symbol(char source)
        {
            Chars = String.Format("{0}", source);
        }
        
        // public bool IsUppercase
        // {
        //     get { return chars != null && chars.Length >= 1 && char.IsLetter(chars[0]) && char.IsUpper(chars[0]); }
        // }
        //
        // public bool IsLower 
        // { 
        //     get { return chars != null && chars.Length >= 1 && char.IsLetter(chars[0]) && char.IsLower(chars[0]); } 
        // }
        
    }
}