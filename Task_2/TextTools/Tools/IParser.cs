using System.IO;
using TextModel;

namespace TextTools.tools
{
    public interface IParser
    {
        Text Parse(Stream stream);
    }
}