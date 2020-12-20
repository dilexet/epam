using TextProcessing.Library.CompositionText;

namespace TextProcessing.Library.Interfaces
{
    public interface ITextStreamWriter
    {
        void TextWriter(string path, Text text);
    }
}