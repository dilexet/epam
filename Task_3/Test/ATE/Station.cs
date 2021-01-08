using System;
using System.Collections.Generic;
using Test.Billing;
using Test.Enums;
using Test.EventArgs;

namespace Test.ATE
{
    public class Station
    {
        private IDictionary<string, Tuple<Terminal, Contract>> _freePorts;
        private IDictionary<string, Tuple<Terminal, Contract>> _offPorts;
        private IDictionary<string, Tuple<Terminal, Contract>> _busyPorts;

        private ICollection<CallInformation> _calls;
        public Station()
        {
            _freePorts = new Dictionary<string, Tuple<Terminal, Contract>>();
            _offPorts = new Dictionary<string, Tuple<Terminal, Contract>>();
            _busyPorts = new Dictionary<string, Tuple<Terminal, Contract>>();
            _calls = new List<CallInformation>();
        }

        public Contract ConcludeContract(Client client, TariffType tariffType)
        {
            return new Contract(client, tariffType);
        }

        public Terminal GetNewTerminal(Contract contract)
        {
            Port port = new Port();
            string terminalNumber = GetTerminalNumber();
            Terminal terminal = new Terminal(terminalNumber, port);
            
            terminal.CallEvent += Call;
            terminal.AnswerEvent += Answer;
            terminal.DropEvent += Drop;

            terminal.ConnectEvent += ConnectToPort;
            terminal.DisconnectEvent += DisconnectToPort;

            _offPorts.Add(terminalNumber, new Tuple<Terminal, Contract>(terminal, contract));
            
            return terminal;
        }
        
        private string GetTerminalNumber()
        {
            Random random = new Random();
            string terminalNumber;
            do
            {
                terminalNumber = $"+375 (27) {random.Next(1000000, 9999999)}";
            } while (_offPorts.ContainsKey(terminalNumber));

            return terminalNumber;
        }

        private void ConnectToPort(Terminal terminal)
        {
            terminal.TerminalPort.Connect();
            if (_offPorts.ContainsKey(terminal.TerminalNumber))
            {
                _freePorts.Add(terminal.TerminalNumber,
                    new Tuple<Terminal, Contract>(_offPorts[terminal.TerminalNumber].Item1,
                        _offPorts[terminal.TerminalNumber].Item2));
                _offPorts.Remove(terminal.TerminalNumber);
            }
        }
        private void DisconnectToPort(Terminal terminal)
        {
            terminal.TerminalPort.Disconnect();
            if (_freePorts.ContainsKey(terminal.TerminalNumber))
            {
                _offPorts.Add(terminal.TerminalNumber,
                    new Tuple<Terminal, Contract>(_freePorts[terminal.TerminalNumber].Item1,
                        _freePorts[terminal.TerminalNumber].Item2));
                _freePorts.Remove(terminal.TerminalNumber);
            }
        }
        
        private void Call(object sender, CallEventArgs e)
        {
            if (_busyPorts.ContainsKey(e.TargetNumberTerminal) || _offPorts.ContainsKey(e.TargetNumberTerminal))
            {
                Console.WriteLine("Вызываемый абонент занят или отключен");
                if (sender is Terminal terminal)
                {
                    terminal.DropCall();
                }
            }
            else if (_freePorts.ContainsKey(e.CallerNumberTerminal) && _freePorts.ContainsKey(e.TargetNumberTerminal))
            {
                Console.WriteLine("Вызываемый аобонент свободен, ожидаем ответа...");
                Console.WriteLine(
                    $"Входящий вызов на номер {e.TargetNumberTerminal} с терминала с номером {e.CallerNumberTerminal}");
                Console.WriteLine("Ответить? Y/N");
                char k = Console.ReadKey().KeyChar;
                if (k == 'Y' || k == 'y')
                {
                    Answer(sender, new AnswerEventArgs(e.CallerNumberTerminal, e.TargetNumberTerminal));
                }
                else if (k == 'N' || k == 'n')
                {
                    Drop(sender, new DropEventArgs(e.CallerNumberTerminal));
                }
            }
        }

        private void Answer(object sender, AnswerEventArgs e)
        {
            if (_freePorts.ContainsKey(e.CallerNumberTerminal) && _freePorts.ContainsKey(e.TargetNumberTerminal))
            {
                _busyPorts.Add(e.CallerNumberTerminal,
                        new Tuple<Terminal, Contract>(_freePorts[e.CallerNumberTerminal].Item1,
                            _freePorts[e.CallerNumberTerminal].Item2));
                _busyPorts.Add(e.TargetNumberTerminal,
                    new Tuple<Terminal, Contract>(_freePorts[e.TargetNumberTerminal].Item1,
                        _freePorts[e.TargetNumberTerminal].Item2));
                
                _freePorts.Remove(e.CallerNumberTerminal);
                _freePorts.Remove(e.TargetNumberTerminal);
            }
        }

        private void Drop(object sender, DropEventArgs e)
        {
            
        }
    }
}