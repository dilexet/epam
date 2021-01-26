using System;
using System.Collections.Generic;
using System.Data.Common;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Sale> SaleRepository { get; }
        void Commit(IEnumerable<Sale> sales);
        DbTransaction CreateTransaction();
        void SaveChange();
    }
}