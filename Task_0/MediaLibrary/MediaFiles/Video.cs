using System;
using System.Drawing;
using System.IO;

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
            /*
            using (var fileStream = new FileStream(Convert.ToString(Location), FileMode.Open)) 
            {
                using (var ms = new MemoryStream(fileStream.ReadByte()))
                {
                    using (var bitmap = new Bitmap(ms))
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            for (int y = 0; y < bitmap.Height; y++) 
                            {
                                Color pixelColor = bitmap.GetPixel(x, y);
                                Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                                bitmap.SetPixel(x, y, newColor);
                            }
                        }

                        // Set the PictureBox to display the image.
                        
                        // PictureBox.Image = bitmap;

                        // Display the pixel format in Label.
                        
                        // Label.Text = "Pixel format: " + image1.PixelFormat.ToString();
                    }
                }
            }
            */
        }
    }
}