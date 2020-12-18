using System;

namespace TextProcessing.Library.CompositionText
{
    public class Symbol
    {
        public string Chars { get; }

        private readonly char[] _vowelList = { 'a', 'A', 'e', 'E', 'u', 
            'U', 'i', 'I', 'o', 'O', 'а', 'А', 'е', 'Е', 'ё', 'Ё', 'и', 
            'И', 'о', 'О', 'у', 'У', 'э', 'Э', 'ы', 'Ы', 'ю', 'Ю', 'я', 'Я' };

        public bool IsConconat
        {
            get
            {
                foreach (var letter in _vowelList)
                {
                    if (Chars == letter.ToString())
                        return false;
                }
                return true;
            }
        }
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