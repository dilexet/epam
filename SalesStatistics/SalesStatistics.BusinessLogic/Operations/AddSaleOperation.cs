using SalesStatistics.DataAccessLayer;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.BusinessLogic.Operations
{
    public class AddSaleOperation : IUnitOfWork
    {
        public IRepository<Sale> Sales { get; set; }

        public AddSaleOperation(IRepository<Sale> sales)
        {
            Sales = sales;
        }
        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}