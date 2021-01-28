using System;
using System.Data.Common;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Sale> SaleRepository { get; }
        IRepository<Manager> ManagerRepository { get; }
        // void Commit(IEnumerable<Sale> sales);
        DbTransaction CreateTransaction();
        void SaveChange();
    }
}