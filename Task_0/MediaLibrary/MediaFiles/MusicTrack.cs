using System;
using System.Media;

namespace MediaLibrary.MediaFiles
{
    internal class MusicTrack : MediaFile
    {
        private readonly string _genre;
        private readonly string _artist;
        private readonly string _album;
        private readonly string _releaseYear;
        private readonly string _length;
        
        public MusicTrack(string name, string type, Uri location, float size, DateTime dateCreation,
            string length,
            string genre, string artist, string album, string releaseYear) 
            : base(name, type, location, size, dateCreation)
        {
            _genre = genre;
            _artist = artist;
            _album = album;
            _releaseYear = releaseYear;
            _length = length;
        }

        public override void Play()
        {
            Console.WriteLine(
                $"\nPlaying MusicTrack: {Name};\nGenre: {_genre};\nArtist: {_artist};\nAlbum: {_album};\nRelease Year: {_releaseYear};\nLength: {_length};");
            /*
            using (var player = new SoundPlayer(Convert.ToString(Location))) // заменить интерфейсом
            {
                player.Play();
            }
            */
        }
    }
}