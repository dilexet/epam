using System;

namespace SalesStatistics.BusinessLogic
{
    public interface IDirectoryWatcher: IDisposable
    {
        IFileHandler FileHandler { get; }
        void Start();
        void Stop();
    }
}