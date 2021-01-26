using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer.UnitOfWork.Operations
{
    public class AddSaleOperation : IUnitOfWork
    {
        public IRepository<Sale> SaleRepository { get; }
        public IRepository<Manager> ManagerRepository { get; }
        
        private ReaderWriterLockSlim Locker { get; }
       
        public AddSaleOperation(IRepository<Sale> saleRepository, IRepository<Manager> managerRepository)
        {
            SaleRepository = saleRepository;
            ManagerRepository = managerRepository;
            Locker = new ReaderWriterLockSlim();
        }

        public void Commit(IEnumerable<Sale> sales)
        {
            Locker.EnterWriteLock();
            try
            {
                if (ManagerRepository.Get(manager => manager.Surname == sales.First().Manager.Surname) != null)
                {
                    foreach (var sale in sales)
                    {
                        SaleRepository.Add(sale);
                    }
                    
                    // TODO: сделать сохрание в другом классе, к тому же с проверками
                    SaveChange();
                }
            }
            finally
            {
                Locker.ExitWriteLock();
            }
            
        }

        public DbTransaction CreateTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void SaveChange()
        {
            SaleRepository.Save();
        }
        
        private bool _disposed;

        ~AddSaleOperation()
        {
            Dispose();
        }
        
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    SaleRepository.Dispose();
                    ManagerRepository.Dispose();
                    Locker.Dispose();
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