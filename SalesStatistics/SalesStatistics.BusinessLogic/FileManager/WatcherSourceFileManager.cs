using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

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
                if (directoryPath == null)
                {
                    Log.Error("DirectoryPath is null");
                }
                if (filesFilter == null)
                {
                    Log.Error("FilesFilter is null");
                }
                if (fileHandler == null)
                {
                    Log.Error("FileHandler is null");
                }
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
            catch (ArgumentException e)
            {
                Log.Error("Check out Path to Directory to Track in AppConfig file: {Source}; {Message}", e.Source, e.Message);
            }
        }

        public void Start()
        {
            _fileSystemWatcher.Created += FileHandler.ProcessFileHandler;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            Task.WaitAll();
            _fileSystemWatcher.Created -= FileHandler.ProcessFileHandler;
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