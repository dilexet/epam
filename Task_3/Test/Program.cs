using System.Threading;
using ATS;
using ATS.Enums;
using BillingSystem;

namespace Test
{
    internal class Program
    {
        public static void Main()
        {
            Billing billing = new Billing();
            
            Station station = new Station();
            
            Client client1 = new Client("Пётр Первый");
            Client client2 = new Client("Иван Грозный");
            Client client3 = new Client("Екатерина Вторая");

            Contract contract1 = station.ConcludeContract(client1, TariffType.Standart);
            Contract contract2 = station.ConcludeContract(client2, TariffType.Standart);
            Contract contract3 = station.ConcludeContract(client3, TariffType.Standart);

            Terminal terminal1 = contract1.Terminal;
            Terminal terminal2 = contract2.Terminal;
            Terminal terminal3 = contract3.Terminal;
            
            terminal1.ConnectToPort();
            terminal2.ConnectToPort();
            terminal3.ConnectToPort();

            terminal1.CallTo(terminal3.TerminalNumber);
            terminal2.CallTo(terminal3.TerminalNumber);
            terminal1.AnswerToCall();
            Thread.Sleep(2000);
            terminal1.DropCall();
            
            
            Thread.Sleep(3000);
            terminal2.DropCall();
            
            terminal2.DisconnectFromPort();
        }
    }
}