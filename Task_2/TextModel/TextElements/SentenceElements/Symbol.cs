namespace TextModel.TextElements.SentenceElements
{
    public class Symbol
    {
        public string Chars { get; }
        private readonly char[] _vowelList = { 'a', 'A', 'e', 'E', 'u', 
            'U', 'i', 'I', 'o', 'O', 'а', 'А', 'е', 'Е', 'ё', 'Ё', 'и', 
            'И', 'о', 'О', 'у', 'У', 'э', 'Э', 'ы', 'Ы', 'ю', 'Ю', 'я', 'Я' };
        public bool IsConsonant
        {
            get
            {
                foreach (var letter in _vowelList)
                {
                    if (Chars == letter.ToString())
                    {
                        return false;
                    }
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
            Chars = $"{source}";
        }
    }
}