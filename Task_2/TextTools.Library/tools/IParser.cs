using System.IO;
using TextModel.Library;

namespace TextTools.Library.tools
{
    public interface IParser
    {
        Text Parse(Stream stream);
    }
}