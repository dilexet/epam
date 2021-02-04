using System;
using SalesStatistics.DataAccessLayer;

namespace SalesStatistics.BusinessLogic.Controller
{
    public sealed class SalesController: IController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDirectoryWatcher _watcher;

        public SalesController(IDirectoryWatcher watcher, IUnitOfWork unitOfWork)
        {
            _watcher = watcher;
            _unitOfWork = unitOfWork;
            _watcher.FileHandler.AddItemsDbEvent += _unitOfWork.Add;
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
                    _unitOfWork.Dispose();
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