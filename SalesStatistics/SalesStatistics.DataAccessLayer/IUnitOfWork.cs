namespace SalesStatistics.DataAccessLayer
{
    public interface IUnitOfWork
    {
        void Commit();
        void Execute();
        void Rollback();
    }
}