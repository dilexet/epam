using System;
using System.IO;

namespace TextTools.tools
{
    public interface ITextStreamReader : IDisposable
    {
        FileStream TextReader();
    }
}