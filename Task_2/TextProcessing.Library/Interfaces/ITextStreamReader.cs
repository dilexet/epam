using TextProcessing.Library.CompositionText;

namespace TextProcessing.Library.Interfaces
{
    public interface ITextStreamReader
    {
        Text TextReader(string path, IParser parser);
    }
}