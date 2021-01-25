using System;

namespace SalesStatistics.BusinessLogic
{
    public interface IDirectoryWatcher: IDisposable
    {
        void Start();
        void Stop();
    }
}