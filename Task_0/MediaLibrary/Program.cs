using System;
using MediaLibrary.Interfaces;
using MediaLibrary.Media;
using MediaLibrary.MediaFiles;


namespace MediaLibrary
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MediaFile mediaFile1 = 
                new Photo("Photo1", "png", new Uri(@"C:\MyPhotos"), 5.2f, DateTime.Now, 1920, 1080);
            
            
            MediaFile mediaFile2 =
                new Video("Video1", "mp4", new Uri(@"C:\MyVideo"), 293.5f, DateTime.Now, "1:20:44", 1920, 1080);
            
            MediaFile mediaFile3 =
                new MusicTrack("Finish Line", "mp3", new Uri(@"C:\MyMusic"), 10.2f, DateTime.Now, "3:26", "Rok", "Skillet",
                    "Victorious", "2019");

            IPlayList playList = new PlayList("MyPlayList");
            playList.AddFile(mediaFile1);
            playList.AddFile(mediaFile2);

            IMediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Play(playList);
            mediaPlayer.Play(mediaFile3);
           
        }
    }
}