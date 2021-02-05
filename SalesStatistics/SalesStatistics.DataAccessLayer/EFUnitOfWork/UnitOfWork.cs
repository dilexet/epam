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
        private IRepository<Sale> _saleRepository;
        private IRepository<Manager> _managerRepository;
        public IRepository<Sale> SaleRepository => _saleRepository ?? (_saleRepository = new GenericRepository<Sale>(_db));
        public IRepository<Manager> ManagerRepository => _managerRepository ?? (_managerRepository = new GenericRepository<Manager>(_db));

        public UnitOfWork(SampleContextFactory contextFactory)
        {
            _db = contextFactory.Create();
        }
        
        public void Add(Manager manager)
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(Locker, ref acquiredLock);
                if (IsManagerInDb(manager))
                {
                    Log.Information("The manager is already in the database");
                    if (acquiredLock)
                    {
                        Monitor.Exit(Locker);
                    }
                    return;
                }
                ManagerRepository.Add(manager);
                foreach (var sale in manager.Sales)
                {
                    SaleRepository.Add(sale);
                }
                SaveChanges();
            }
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (Exception e)
            {
                Log.Error("Information is not Added to Database: {Message}", e.Message);
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(Locker);
                }
            }
        }

        private bool IsManagerInDb(Manager manager)
        {
            var item = ManagerRepository.SingleOrDefault(x => x.Surname == manager.Surname);
            return item != null;
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