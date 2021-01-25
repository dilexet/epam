using System;
using System.IO;

namespace SalesStatistics.BusinessLogic.FileManager
{
    public class WatcherSourceFileManager: IDirectoryWatcher
    {
        private FileSystemWatcher _watcher;

        public WatcherSourceFileManager(string directoryPath, string filesFilter)
        {
            _watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                Filter = filesFilter,
                NotifyFilter = NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName
                                       | NotifyFilters.DirectoryName
            };
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
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