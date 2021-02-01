using System;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Sale> SaleRepository { get; }
        IRepository<Manager> ManagerRepository { get; }
        void SaveChange();
    }
}