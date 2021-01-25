namespace SalesStatistics.BusinessLogic
{
    public interface IUnitOfWork
    {
        void Commit();
        void Execute();
        void Rollback();
    }
}