using System;

namespace SalesStatistics.BusinessLogic.Controller
{
    public sealed class SalesController: IController
    {
        private IDirectoryWatcher _watcher;
        public SalesController(IDirectoryWatcher watcher)
        {
            _watcher = watcher;
          
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