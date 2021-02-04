﻿using System;
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
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (ArgumentException e)
            {
                Log.Error("Check out Path to Directory to Track in AppConfig file: {Message}", e.Message);
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