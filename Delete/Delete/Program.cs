using System;
using System.Threading;
using Delete.AutomaticTelephoneExchange;
using Delete.BillingSystem;
using Delete.Enums;
using Delete.Interfaces;

namespace Delete
{
    class Program
    {
        static void Main()
        {
            IAte ate = new Ate();
            IReportRender render = new ReportRender();
            IBillingSystem bs = new Delete.BillingSystem.BillingSystem(ate);

            IContract c1 = ate.RegisterContract(new Subscriber("Vasia", "Pupkin"), TariffType.Light);
            IContract c2 = ate.RegisterContract(new Subscriber("Dima", "Pupkin"), TariffType.Light);
            IContract c3 = ate.RegisterContract(new Subscriber("Petya", "Pupkin"), TariffType.Light);

            c1.Subscriber.AddMoney(10);
            var t1 = ate.GetNewTerminal(c1);
            var t2 = ate.GetNewTerminal(c2);
            var t3 = ate.GetNewTerminal(c3);
            t1.ConnectToPort();
            t2.ConnectToPort();
            t3.ConnectToPort();
            t1.Call(t2.Number); 
            Thread.Sleep(2000);
            t2.EndCall();
            t3.Call(t1.Number);
            Thread.Sleep(1000);
            t3.EndCall();
            t2.Call(t1.Number);
            Thread.Sleep(3000);
            t1.EndCall();

            Console.WriteLine();
            Console.WriteLine("Sorted records:");
            foreach (var item in render.SortCalls(bs.GetReport(t1.Number), TypeSort.SortByCallType))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }

            Console.ReadKey();
            

        }
    }
}
