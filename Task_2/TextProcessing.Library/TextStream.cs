using System;
using System.IO;
using TextProcessing.Library.CompositionText;
using TextProcessing.Library.Interfaces;

namespace TextProcessing.Library
{
    public class TextStream : ITextStreamReader, ITextStreamWriter
    {
        public Text TextReader(string path, IParser parser)
        {
            if (string.IsNullOrEmpty(path)) 
                throw new NullReferenceException("The file path is incorrect");
            try
            {
                using (FileStream fileStream = File.OpenRead(path))
                {
                    return parser.Parse(fileStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void TextWriter(string path, Text text)
        {
            if (string.IsNullOrEmpty(path)) 
                throw new NullReferenceException("The file path is incorrect");
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text.ToString());
                    fileStream.Write(array, 0, array.Length);
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