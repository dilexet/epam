using System;
using System.Collections.Generic;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Sale> SaleRepository { get; }
        IRepository<Manager> ManagerRepository { get; }
        void Add(IEnumerable<Sale> sales, Manager manager);
        void SaveChanges();
    }
}