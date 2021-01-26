using System;

namespace SalesStatistics.BusinessLogic
{
    public sealed class SalesController: IController
    {
        private IDirectoryWatcher _watcher;
        private IFileHandler _fileHandler;
        public SalesController(IDirectoryWatcher watcher, IFileHandler fileHandler)
        {
            _watcher = watcher;
            _fileHandler = fileHandler;
        }

        public void Start()
        {
            _watcher.Start(_fileHandler);
        }

        public void Stop()
        {
            _watcher.Stop(_fileHandler);
        }
        
        private bool _disposed;

        ~SalesController()
        {
            Dispose();
        }
        
        private void Dispose(bool disposing)
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