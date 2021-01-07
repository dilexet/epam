using Delete.Args;
using Delete.AutomaticTelephoneExchange;
using Delete.BillingSystem;
using Delete.Enums;

namespace Delete.Interfaces
{
    public interface IAte : IStorage<CallInformation>
    {
        Terminal GetNewTerminal(IContract contract);
        IContract RegisterContract(Subscriber subscriber, TariffType type);
        void CallingTo(object sender, ICallingEventArgs e);
    }
}
