using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextProcessing.Library.CompositionText
{
    public class Text
    {
        private IEnumerable<Sentence> _sentences;

        public Text(IEnumerable<Sentence> sentences)
        {
            _sentences = sentences;
        }

      
    }
}