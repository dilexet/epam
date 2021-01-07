using System;
using System.Collections.Generic;
using Test.Billing;
using Test.Enums;
using Test.EventArgs;

namespace Test.ATE
{
    public class Station
    {
        private IDictionary<string, Port> _allPorts;

        public Station()
        {
            _allPorts = new Dictionary<string, Port>();
        }

        public Contract ConcludeContract(Client client, TariffType tariffType)
        {
            return new Contract(client, tariffType);
        }

        public Terminal GetNewTerminal()
        {
            Port port = new Port();
            string terminalNumber = GetTerminalNumber();
            Terminal terminal = new Terminal(terminalNumber, port);
            
            terminal.CallEvent += Call;
            terminal.AnswerEvent += Call;
            terminal.DropEvent += Call;
            
            _allPorts.Add(terminalNumber, port);
            
            return terminal;
        }
        private string GetTerminalNumber()
        {
            Random random = new Random();
            string terminalNumber;
            do
            {
                terminalNumber = $"+375 (27) {random.Next(1000000, 9999999)}";
            } while (_allPorts.ContainsKey(terminalNumber));

            return terminalNumber;
        }

        private void Call(object sender, ICallEventsArgs e)
        {
            if (e is CallEventArgs)
            {
                Console.WriteLine("Call");
            }

            if (e is AnswerEventArgs)
            {
                Console.WriteLine("Answer");
            }

            if (e is DropEventArgs)
            {
                Console.WriteLine("Drop");
            }
        }
    }
}