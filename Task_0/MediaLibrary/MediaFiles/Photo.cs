using System;

namespace MediaLibrary.MediaFiles
{
    internal class Photo : MediaFile
    {
        private readonly int _width;
        private readonly int _height;
        
        public Photo(string name, string type, Uri location, float size, DateTime dateCreation, 
            int width, int height) 
            : base(name, type, location, size, dateCreation)
        {
            _width = width;
            _height = height;
        }
        
        public override void Play()
        {
            Console.WriteLine($"\nPlaying Photo: {Name} ({_width}x{_height})");
        }
        
    }
}