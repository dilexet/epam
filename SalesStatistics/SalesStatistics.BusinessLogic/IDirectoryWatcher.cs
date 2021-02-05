using System;
using SalesStatistics.BusinessLogic.FileManager;

namespace SalesStatistics.BusinessLogic
{
    public interface IDirectoryWatcher: IDisposable
    {
        FileHandler FileHandler { get; }
        
        void Start();
        
        void Stop();
    }
}