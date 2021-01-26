using System;

namespace SalesStatistics.BusinessLogic
{
    public interface IDirectoryWatcher: IDisposable
    {
        void Start(IFileHandler fileHandler);
        void Stop(IFileHandler fileHandler);
    }
}