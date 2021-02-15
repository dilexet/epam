using System;

namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository Repository { get; }
        void SaveChanges();
    }
}