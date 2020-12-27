using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextModel;
using TextModel.TextElements.SentenceElements;

namespace TextConcordance
{
    public class Concordance
    {
        private readonly ICollection<Word> _wordsSortCollection;
        
        private readonly ICollection<string> _pagesCollection;
        
        private readonly int _numberOfLinesPerPage;
        
        private readonly Text _text;
        
        private readonly string _patternNewLine;
        
        public Concordance(Text text, int numberOfLinesPerPage, string patternNewLine)
        {
            _text = text;
            _numberOfLinesPerPage = numberOfLinesPerPage;
            _patternNewLine = patternNewLine;
            _pagesCollection = SplitTextIntoPages();
            _wordsSortCollection = GetWordCollection(text);
        }
        
        public string GetConcordance()
        {
            int bufferlength = 10000;
            StringBuilder stringBuilder = new StringBuilder(bufferlength);
            const char dot = '.';
            const int countDot = 50;
            string capitalLetter = "";
            foreach (var word in _wordsSortCollection)
            {
                if (capitalLetter.ToLower() != word.Value[0].ToString())
                {
                    capitalLetter = word.Value[0].ToString().ToUpper();
                    stringBuilder.Append($"{capitalLetter}\r\n");
                }
                stringBuilder.Append(word.Value);
                for (int i = 0; i < countDot - word.SymbolCount; i++)
                {
                    stringBuilder.Append(dot);
                }
                stringBuilder.Append(NumberOccurrencesOfWordInText(word.Value));
                stringBuilder.Append($": {GetPageNumber(word.Value)}");
                stringBuilder.Append("\r\n");
            }
            return stringBuilder.ToString();
        }
        
        private int NumberOccurrencesOfWordInText(string word)
        {
            int count = 0;
            foreach (var sentence in _text.GetSentence())
            {
                count += sentence.GetSentenceItems().OfType<Word>()
                    .Count(item => item.Value.ToLower() == word.ToLower());
            }
            return count;
        }
        
        private string GetPageNumber(string word)
        {
            string pageNumber = "";
            int page = 1;
            foreach (var line in _pagesCollection)
            {
                if (Regex.IsMatch(line, $@"((\W+)({word})(\W*))|((\W*)({word})(\W+))", RegexOptions.IgnoreCase)) 
                {
                    pageNumber += page + " ";
                }
                page++;
            }
            return pageNumber;
        }
        
        private ICollection<Word> GetWordCollection(Text text)
        {
            ICollection<Word> words = new List<Word>();
            foreach (var sentence in text.GetSentence())
            {
                foreach (var word in sentence.GetSentenceItems().OfType<Word>())
                {
                    words.Add(new Word(word.Value.ToLower()));
                }
            }
            return SortListWord(words);
        }
        
        private ICollection<Word> SortListWord(ICollection<Word> words)
        {
            var items = words.Distinct().ToList();
            items.Sort();
            return items;
        }

        private ICollection<string> SplitTextIntoPages()
        {
            int bufferLenght = 10000;
            StringBuilder lines = new StringBuilder(bufferLenght);
            lines.Clear();
            ICollection<string> pages = new List<string>();
            int numberLine = 0;
            foreach (var line in Regex.Split(_text.ToString(), _patternNewLine)) 
            {
                if (numberLine >= _numberOfLinesPerPage)
                {
                    pages.Add(lines.ToString());
                    lines.Clear();
                    numberLine = 0;
                }
                lines.Append(line);
                numberLine++;
            }
            if (!string.IsNullOrEmpty(lines.ToString()))
            {
                pages.Add(lines.ToString());
                lines.Clear();
            }
            return pages;
        }
    }
}