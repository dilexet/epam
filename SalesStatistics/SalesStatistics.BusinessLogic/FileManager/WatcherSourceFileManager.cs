using System;
using System.IO;
using System.Threading.Tasks;

namespace SalesStatistics.BusinessLogic.FileManager
{
    public sealed class WatcherSourceFileManager: IDirectoryWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        public IFileHandler FileHandler { get; }

        public WatcherSourceFileManager(string directoryPath, string filesFilter, IFileHandler fileHandler)
        {
            try
            {
                FileHandler = fileHandler;
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
            catch (ArgumentException)
            {
                throw new ArgumentException("Check out Path to Directory to Track in AppConfig file.");
            }
        }

        public void Start()
        {
            try
            {
                _fileSystemWatcher.Created += FileHandler.ProcessFileHandler;
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                Task.WaitAll();
                _fileSystemWatcher.Created -= FileHandler.ProcessFileHandler;
                _fileSystemWatcher.EnableRaisingEvents = false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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