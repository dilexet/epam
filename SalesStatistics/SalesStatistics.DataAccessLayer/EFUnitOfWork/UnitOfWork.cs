using System;
using System.Data.Common;
using System.Threading;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer.EFUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesInformationContext _db = new SalesInformationContext();
        private IRepository<Sale> _saleRepository;
        private IRepository<Manager> _managerRepository;
        
        public IRepository<Sale> SaleRepository
        {
            get
            {
                if (_saleRepository == null)
                {
                    _saleRepository = new GenericRepository<Sale>(_db);
                }
                return _saleRepository;
            }
        }

        public IRepository<Manager> ManagerRepository
        {
            get
            {
                if (_managerRepository == null)
                {
                    _managerRepository = new GenericRepository<Manager>(_db);
                }
                return _managerRepository;
            }
        }

        private ReaderWriterLockSlim Locker { get; }
       
        public UnitOfWork()
        {
            Locker = new ReaderWriterLockSlim();
        }

        /*public void Commit(IEnumerable<Sale> sales)
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
                }
            }
            finally
            {
                Locker.ExitWriteLock();
            }
            
        }*/

        public DbTransaction CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChange()
        {
            _db.SaveChanges();
        }
        
        private bool _disposed;

        ~UnitOfWork()
        {
            Dispose();
        }
        
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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