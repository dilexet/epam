using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
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

        public UnitOfWork(string connectionString)
        {
            _db = new SalesInformationContext(connectionString);
        }
        
        public void Add(IEnumerable<Sale> sales, Manager manager)
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(Locker, ref acquiredLock);
                foreach (var sale in sales)
                {
                    SaleRepository.Add(sale);
                }

                ManagerRepository.Add(manager);
                SaveChanges();
            }
            catch (EntitySqlException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (SqlException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (EntityException e)
            {
                Log.Error("{Message}", e.Message);
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

        public void SaveChanges()
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