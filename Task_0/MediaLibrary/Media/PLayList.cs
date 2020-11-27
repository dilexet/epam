using System.Collections.Generic;
using MediaLibrary.Interfaces;
using MediaLibrary.MediaFiles;

namespace MediaLibrary.Media
{
    internal class PlayList : IPlayList
    {
        protected string Name;

        protected ICollection<MediaFile> mediaFiles;

        public PlayList(string name)
        {
            Name = name;
            mediaFiles = new List<MediaFile>();
        }
        
        public void AddFile(MediaFile mediaFile)
        {
            mediaFiles.Add(mediaFile);
        }

        public void RemoveFile(MediaFile mediaFile)
        {
            mediaFiles.Remove(mediaFile);
        }

        public void ClearAllFiles()
        {
            mediaFiles.Clear();
        }

        public MediaFile SearchFile(string name)
        {
            foreach (var mediaFile in mediaFiles)
            {
                if (mediaFile.Name.Contains(name))
                    return mediaFile;
            }
            return null;
        }
        
        public ICollection<MediaFile> GetMediaFiles()
        {
            return mediaFiles;
        }
    }
}