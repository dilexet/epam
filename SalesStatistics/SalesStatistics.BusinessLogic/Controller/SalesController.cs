﻿using System;
using System.Collections.Generic;
using System.Threading;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.BusinessLogic.Controller
{
    public sealed class SalesController: IController
    {
        private ReaderWriterLockSlim _locker;
        private IUnitOfWork _unitOfWork;
        private IDirectoryWatcher _watcher;
        public SalesController(IDirectoryWatcher watcher, IUnitOfWork unitOfWork)
        {
            _watcher = watcher;
            _unitOfWork = unitOfWork;
            _locker = new ReaderWriterLockSlim();
            _watcher.FileHandler.AddItemsDbEvent += FileHandlerOnAddItemsDbEvent;
        }

        private void FileHandlerOnAddItemsDbEvent(IEnumerable<Sale> sales, Manager manager)
        {
            _locker.EnterWriteLock();
            try
            {
                foreach (var sale in sales)
                {
                    _unitOfWork.SaleRepository.Add(sale);
                }

                _unitOfWork.ManagerRepository.Add(manager);
                _unitOfWork.SaveChange();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
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