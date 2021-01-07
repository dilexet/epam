using Delete.BillingSystem;

namespace Delete.Interfaces
{
    public interface IBillingSystem
    {
        Report GetReport(int telephoneNumber);
    }
}
