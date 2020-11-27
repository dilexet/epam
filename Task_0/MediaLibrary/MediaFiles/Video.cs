using System;

namespace MediaLibrary.MediaFiles
{
    internal class Video : MediaFile
    {
        private readonly int _width;
        private readonly int _height;
        private readonly string _length;
        public Video(string name, string type, Uri location, float size, DateTime dateCreation,
            string length,
            int width, int height)
            : base(name, type, location, size, dateCreation)
        {
            _width = width;
            _height = height;
            _length = length;
        }
        
        public override void Play()
        {
            Console.WriteLine($"\nPlaying Video: {Name} ({_width}x{_height});\nLength: {_length}");
        }
    }
}