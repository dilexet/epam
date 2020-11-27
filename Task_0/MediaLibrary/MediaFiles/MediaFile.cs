using System;

namespace MediaLibrary.MediaFiles
{
    internal abstract class MediaFile
    {
        public readonly string Name;
        protected readonly string Type;
        protected readonly Uri Location;
        protected readonly float Size;
        protected readonly DateTime DateCreation;

        protected MediaFile(string name, string type, Uri location, float size, DateTime dateCreation)
        {
            Name = name;
            Type = type;
            Location = location;
            Size = size;
            DateCreation = dateCreation;
        }

        public abstract void Play();
        
    }
}