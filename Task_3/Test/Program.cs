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
            
            Station station = new Station(billing);
            
            
            Client client1 = new Client("Пётр Первый");
            Client client2 = new Client("Иван Грозный");
            Client client3 = new Client("Екатерина Вторая");
            
            station.ConcludeContract(client1, TariffType.Standart);
            station.ConcludeContract(client2, TariffType.Standart);
            station.ConcludeContract(client3, TariffType.Standart);

            client1.Terminal.ConnectToPort();
            client2.Terminal.ConnectToPort();
            client3.Terminal.ConnectToPort();

            client1.Terminal.CallTo(client3.Terminal.TerminalNumber);
            Thread.Sleep(2000);
            client1.Terminal.DropCall();
            
            client3.Terminal.CallTo(client2.Terminal.TerminalNumber);
            Thread.Sleep(3000);
            client2.Terminal.DropCall();
            
            client2.Terminal.DisconnectFromPort();
        }
    }
}