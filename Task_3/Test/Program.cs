using System;
using System.Threading;
using AutomaticTelephoneStation.ATS;
using AutomaticTelephoneStation.ATS.Interfaces;
using AutomaticTelephoneStation.BillingSystem;
using AutomaticTelephoneStation.BillingSystem.Enums;

namespace Test
{
    internal class Program
    {
        public static void Main()
        {
            Billing billing = new Billing();

            Company company = new Company();

            company.ConcludeContractEvent += billing.ConcludeContractHandler;

            Contract contract1 = company.ConcludeContract(
                new Client("Пётр Первый"), 
                new Tariff(TariffType.Standard), 
                "228");
            Contract contract2 = company.ConcludeContract(
                new Client("Иван Грозный"),
                new Tariff(TariffType.Standard), 
                "77 88");
            Contract contract3 = company.ConcludeContract(
                new Client("Екатерина Вторая"),
                new Tariff(TariffType.Standard), 
                "8 800 555 35 35");
            
            ITelephoneStation telephoneStation = new TelephoneStation(company);
            telephoneStation.CallEndedEvent += billing.CallEndedHandler;
            
            contract1.Client.AddMoney(10);
            contract3.Client.AddMoney(10);
            
            Terminal terminal1 = contract1.Terminal;
            terminal1.CallEvent += telephoneStation.CallHandler;
            terminal1.AnswerEvent += telephoneStation.AnswerHandler;
            terminal1.DropEvent += telephoneStation.DropHandler;
            
            Terminal terminal2 = contract2.Terminal;
            terminal2.CallEvent += telephoneStation.CallHandler;
            terminal2.AnswerEvent += telephoneStation.AnswerHandler;
            terminal2.DropEvent += telephoneStation.DropHandler;
            
            Terminal terminal3 = contract3.Terminal;
            terminal3.CallEvent += telephoneStation.CallHandler;
            terminal3.AnswerEvent += telephoneStation.AnswerHandler;
            terminal3.DropEvent += telephoneStation.DropHandler;

            
            
            terminal1.ConnectToPort();
            terminal2.ConnectToPort();
            terminal3.ConnectToPort();

            terminal1.CallTo(terminal3.TerminalPort.Number);
            terminal3.AnswerToCall();
            Thread.Sleep(2000);
            terminal1.DropCall();

            terminal2.CallTo(terminal1.TerminalPort.Number);
            terminal1.AnswerToCall();
            Thread.Sleep(3000);
            terminal2.DropCall();

            Console.WriteLine("\n");
            foreach (var call in billing.FilterNumber(terminal1.TerminalPort.Number))
            {
                Console.WriteLine($"{call}\n");
            }
            
            terminal1.DisconnectFromPort();
        }
    }
}