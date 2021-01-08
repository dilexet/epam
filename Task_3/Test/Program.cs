using Test.ATE;
using Test.Billing;
using Test.Enums;

namespace Test
{
    internal class Program
    {
        public static void Main()
        {
            Station station = new Station();

            Client client1 = new Client("Пётр Первый");
            Client client2 = new Client("Иван Грозный");
            Client client3 = new Client("Екатерина Вторая");
            
            Contract contract1 = station.ConcludeContract(client1, TariffType.Standart);
            Contract contract2 = station.ConcludeContract(client2, TariffType.Standart);
            Contract contract3 = station.ConcludeContract(client3, TariffType.Standart);

            Terminal terminal1 = station.GetNewTerminal(contract1);
            Terminal terminal2 = station.GetNewTerminal(contract2);
            Terminal terminal3 = station.GetNewTerminal(contract3);
            
            terminal1.ConnectToPort();
            terminal2.ConnectToPort();
            terminal3.ConnectToPort();
            
            terminal2.DisconnectFromPort();
            
            terminal2.CallTo(terminal3.TerminalNumber); // оба порта должны быть free
            
            terminal1.CallTo(terminal2.TerminalNumber); // так как порт 2 busy, а порт 1 free, звонок должен сброситься
            
            terminal3.DropCall(); // терминал 3 сбрасывает звонок, и порты 2 и 3 становяться free
            
            terminal1.CallTo(terminal2.TerminalNumber); // терминал 1 снова звонит терминал 2, порт 2 free значит терминал 1 дозвониться
          
        }
    }
}