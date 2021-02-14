using System;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository Repository { get; }
        void Add(Manager manager);
        void SaveChanges();
    }
}