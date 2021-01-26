using System.Data.Common;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork
    {
        IRepository<Sale> SaleRepository { get; }
        void Commit(Sale sale);
        DbTransaction CreateTransaction();
        void SaveChange();
    }
}