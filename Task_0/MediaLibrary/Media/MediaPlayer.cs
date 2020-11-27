using MediaLibrary.Interfaces;
using MediaLibrary.MediaFiles;

namespace MediaLibrary.Media
{
    internal class MediaPlayer : IMediaPlayer
    {
        public void Play(IPlayList playList)
        {
            foreach (var mediaFile in playList.GetMediaFiles())
            {
                mediaFile.Play();
            }
        }
        public void Play(MediaFile mediaFile)
        {
            mediaFile.Play();
        }
    }
}