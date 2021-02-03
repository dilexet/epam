using System.Data.Entity.Infrastructure;

namespace SalesStatistics.DataAccessLayer.EntityFrameworkContext
{
    public class SampleContextFactory : IDbContextFactory<SalesInformationContext>
    {
        public SalesInformationContext Create()
        {
            return new SalesInformationContext("Test");
        }
    }
}