using System;
using System.Threading;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.ModelLayer.Models;
using Serilog;

namespace SalesStatistics.DataAccessLayer.EFUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        static readonly object Locker = new object();

        private readonly SalesInformationContext _db;
        private IRepository _repository;
        public IRepository Repository => _repository ?? (_repository = new GenericRepository(_db));
        

        public UnitOfWork(SampleContextFactory contextFactory)
        {
            _db = contextFactory.Create();
        }
        
        public void SaveChanges()
        {
            try
            {
                _db.SaveChanges();
                Log.Information("Saving to database completed successfully");
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.Message);
            }
        }
        
        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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