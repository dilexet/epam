using System;
using SalesStatistics.BusinessLogic.FileManager;

namespace SalesStatistics.BusinessLogic
{
    public class Controller: IController
    {
        private IDirectoryWatcher _watcher;
        public Controller(string directoryPath, string filesFilter)
        {
            _watcher = new WatcherSourceFileManager(directoryPath, filesFilter);
        }

        public void Start()
        {
            _watcher.Start();
        }

        public void Stop()
        {
            _watcher.Stop();
        }
        
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _watcher.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}