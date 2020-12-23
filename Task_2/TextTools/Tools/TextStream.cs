using System;
using System.IO;
using TextModel;

namespace TextTools.tools
{
    public class TextStream : ITextStreamReader
    {
        private readonly IParser _parser;
        public TextStream(IParser parser)
        {
            _parser = parser;
        }
        public Text TextReader(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new NullReferenceException("The file path is incorrect");
            }
            try
            {
                using (FileStream fileStream = File.OpenRead(path))
                {
                    return _parser.Parse(fileStream);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}