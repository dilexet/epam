using System.Collections.Generic;
using MediaLibrary.MediaFiles;

namespace MediaLibrary.Interfaces
{
    internal interface IPlayList
    {
        void AddFile(MediaFile mediaFile);
        void RemoveFile(MediaFile mediaFile);
        void ClearAllFiles();
        MediaFile SearchFile(string name);
        ICollection<MediaFile> GetMediaFiles();
    }
}