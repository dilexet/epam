using System;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Manager> ManagerRepository { get; }
        IRepository<Sale> SaleRepository { get; }
        void Add(Manager manager);
        void SaveChanges();
    }
}