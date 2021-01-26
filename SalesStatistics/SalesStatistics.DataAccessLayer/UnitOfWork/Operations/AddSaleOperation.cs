using System.Data.Common;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer.UnitOfWork.Operations
{
    public class AddSaleOperation : IUnitOfWork
    {
        public IRepository<Sale> SaleRepository { get; }
        public IRepository<Manager> ManagerRepository { get; }
        public AddSaleOperation(IRepository<Sale> saleRepository, IRepository<Manager> managerRepository)
        {
            SaleRepository = saleRepository;
            ManagerRepository = managerRepository;
        }

        public void Commit(Sale sale)
        {
            // SaleRepository.Add(sale);
        }

        public DbTransaction CreateTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void SaveChange()
        {
            throw new System.NotImplementedException();
        }
    }
}