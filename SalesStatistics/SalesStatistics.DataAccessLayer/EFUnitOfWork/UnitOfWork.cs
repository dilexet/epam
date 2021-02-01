using System;
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
        
        public IRepository<Sale> SaleRepository => _saleRepository ?? (_saleRepository = new GenericRepository<Sale>(_db));

        public IRepository<Manager> ManagerRepository => _managerRepository ?? (_managerRepository = new GenericRepository<Manager>(_db));

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