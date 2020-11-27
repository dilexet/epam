using MediaLibrary.MediaFiles;

namespace MediaLibrary.Interfaces
{
    internal interface IMediaPlayer
    {
        void Play(IPlayList playList);
        void Play(MediaFile mediaFile);
    }
}