using System;
using Test.Enums;

namespace Test
{
    public class Station
    {
        public Contract ConcludeContract(ref Client client, TariffType tariffType)
        {
            client.Terminal = GetTerminal();
            var contract = new Contract(client, tariffType);
            return contract;
        }

        private Terminal GetTerminal()
        {
            Port port = new Port();
            // TODO: подписаться на события порта
            Random random = new Random();
            Terminal terminal = new Terminal($"+375 (27) {random.Next(1000000, 9999999)}", port);
            return terminal;
        }
    }
}