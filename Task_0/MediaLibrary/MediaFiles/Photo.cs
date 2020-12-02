using System;
using System.Drawing;

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
            /*
            using (var bitmap = new Bitmap(Convert.ToString(Location)))
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
            */

        }
        
    }
}