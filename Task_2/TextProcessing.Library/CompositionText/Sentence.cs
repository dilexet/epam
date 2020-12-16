using System.Collections;
using System.Collections.Generic;

namespace TextProcessing.Library.CompositionText
{
    public class Sentence
    {
        private List<Word> _words;
        private List<Punctuation> _punctuations;
        

        public Sentence(List<Word> words, List<Punctuation> punctuations)
        {
            _words = words;
            _punctuations = punctuations;
        }
        
    }
}