using System;
using System.IO;

namespace TextTools.tools
{
    public class TextStream : ITextStreamReader
    {
        private readonly string _path;
        
        private FileStream _stream;
        
        public TextStream(string path)
        {
            _path = path;
        }
        
        public FileStream TextReader()
        {
            if (string.IsNullOrEmpty(_path))
            {
                throw new NullReferenceException("The file path is incorrect");
            }
            try
            {
                _stream = File.OpenRead(_path);
                return _stream;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}