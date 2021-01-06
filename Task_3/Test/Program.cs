using Test.Enums;
using Test.Interfaces;

namespace Test
{
    class Program
    {
        
        static void Main()
        {
            Station station = new Station();

           // BillingHandler billingHandler = new BillingHandler();
            
            Client client1 = new Client("Пётр Первый");
            Client client2 = new Client("Иван Грозный");
            Client client3 = new Client("Екатерина Вторая");
            
            IContract contract1 = station.ConcludeContract(ref client1, TariffType.Standart);
            IContract contract2 = station.ConcludeContract(ref client2, TariffType.Pro);
            IContract contract3 = station.ConcludeContract(ref client3, TariffType.Standart);

            // TODO: добавить подключение к порту
            // client.Terminal.ConnectToPort() - примерно так
            client1.Terminal.Call(client2.Terminal.TerminalNumber);
            // Таким же образом будут работать методы Drop and Answer
        }
    }
}