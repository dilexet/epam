using System.IO;
using TextProcessing.Library.CompositionText;

namespace TextProcessing.Library.Interfaces
{
    public interface IParser
    {
        Text Parse(FileStream fileStream);
    }
}