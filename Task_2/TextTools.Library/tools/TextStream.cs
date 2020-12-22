using System;
using System.IO;
using TextModel.Library;

namespace TextTools.Library.tools
{
    public class TextStream : ITextStreamReader
    {
        private readonly IParser _parser;
        public TextStream()
        {
            _parser = new Parser();
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
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}