using System;
using System.Threading;
using AutomaticTelephoneStation.ATS;
using AutomaticTelephoneStation.BillingSystem;
using AutomaticTelephoneStation.BillingSystem.Enums;
using AutomaticTelephoneStation.BillingSystem.Report;

namespace Test
{
    internal class Program
    {
        public static void Main()
        {
            Billing billing = new Billing();
            
            Station station = new Station();
            station.CallReportEvent += billing.c_CallReport;
            Client client1 = new Client("Пётр Первый");
            Client client2 = new Client("Иван Грозный");
            Client client3 = new Client("Екатерина Вторая");

            Contract contract1 = station.ConcludeContract(client1, new Tariff(TariffType.Standart));
            Contract contract2 = station.ConcludeContract(client2, new Tariff(TariffType.Standart));
            Contract contract3 = station.ConcludeContract(client3, new Tariff(TariffType.Standart));

            contract1.Client.AddMoney(10);
            contract3.Client.AddMoney(10);
            
            Terminal terminal1 = contract1.Terminal;
            Terminal terminal2 = contract2.Terminal;
            Terminal terminal3 = contract3.Terminal;
            
            terminal1.ConnectToPort();
            terminal2.ConnectToPort();
            terminal3.ConnectToPort();

            terminal1.CallTo(terminal3.TerminalNumber);
            terminal3.AnswerToCall();
            Thread.Sleep(2000);
            terminal1.DropCall();

            terminal2.CallTo(terminal1.TerminalNumber);
            terminal1.AnswerToCall();
            Thread.Sleep(3000);
            terminal2.DropCall();

            Console.WriteLine("\n");
            foreach (var call in ReportRender.FilterTerminalNumber(billing.GetReport(), terminal1.TerminalNumber))
            {
                Console.WriteLine($"{call}\n");
            }
            
            terminal1.DisconnectFromPort();
        }
    }
}