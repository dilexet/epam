using System;
using System.IO;

namespace SalesStatistics.BusinessLogic.FileManager
{
    public sealed class WatcherSourceFileManager: IDirectoryWatcher
    {
        private FileSystemWatcher _fileSystemWatcher;
        

        public WatcherSourceFileManager(string directoryPath, string filesFilter)
        {
            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = directoryPath,
                Filter = filesFilter,
                NotifyFilter = NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName
                                       | NotifyFilters.DirectoryName
            };
        }
        
        public void Start(IFileHandler fileHandler)
        {
            try
            {
                _fileSystemWatcher.Created += fileHandler.ProcessFileHandler;
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Stop(IFileHandler fileHandler)
        {
            _fileSystemWatcher.Created -= fileHandler.ProcessFileHandler;
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
        
        private bool _disposed;

        ~WatcherSourceFileManager()
        {
            Dispose();
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileSystemWatcher.Dispose();
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