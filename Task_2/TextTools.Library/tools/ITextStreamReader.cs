using TextModel.Library;

namespace TextTools.Library.tools
{
    public interface ITextStreamReader
    {
        Text TextReader(string path, IParser parser);
    }
}