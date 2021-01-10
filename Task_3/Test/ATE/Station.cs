using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Test.Billing;
using Test.Enums;
using Test.EventArgs;

namespace Test.ATE
{
    public class Station
    {
        public IDictionary<string, Tuple<Terminal, Contract>> Clients { get; }
        public ICollection<CallInformation> Calls { get; }

        public Station()
        {
            Clients = new Dictionary<string, Tuple<Terminal, Contract>>();
            Calls = new List<CallInformation>();
        }

        public Contract ConcludeContract(Client client, TariffType tariffType)
        {
            return new Contract(client, tariffType);;
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

            Clients.Add(terminalNumber, new Tuple<Terminal, Contract>(terminal, contract));
            
            return terminal;
        }
        
        private string GetTerminalNumber()
        {
            Random random = new Random();
            string terminalNumber;
            do
            {
                terminalNumber = $"+375 (27) {random.Next(1000000, 9999999)}";
            } while (Clients.ContainsKey(terminalNumber));

            return terminalNumber;
        }

        private void ConnectToPort(Terminal terminal)
        {
            terminal.TerminalPort.Connect();
            if (Clients.ContainsKey(terminal.TerminalNumber))
            {
                Clients[terminal.TerminalNumber].Item1.TerminalPort.State = PortState.Free;
            }
        }
        private void DisconnectToPort(Terminal terminal)
        {
            terminal.TerminalPort.Disconnect();
            if (Clients.ContainsKey(terminal.TerminalNumber))
            {
                Clients[terminal.TerminalNumber].Item1.TerminalPort.State = PortState.Off;
            }
        }

        private void Call(object sender, CallEventArgs e)
        {
            Terminal caller = Clients[e.CallerNumberTerminal].Item1;
            Terminal target = Clients[e.TargetNumberTerminal].Item1;

            if (caller != null && target != null)
            {
                if (target.TerminalPort.State != PortState.Free)
                {
                    Console.WriteLine("Вызываемый абонент занят или отключен");
                    target.DropCall();
                }
                else if (caller.TerminalPort.State != PortState.Free)
                {
                    Console.WriteLine("Ваш терминал отключен или занят");
                    caller.DropCall();
                }
                else
                {
                    Console.WriteLine("Вызываемый аобонент свободен, ожидаем ответа...");
                    Console.WriteLine(
                        $"Входящий вызов на номер {e.TargetNumberTerminal} с терминала с номером {e.CallerNumberTerminal}");
                    Console.WriteLine("Ответить? y/n");
                    char k = Console.ReadKey(true).KeyChar;
                    switch (k)
                    {
                        case 'y':
                            target.AnswerToCall(e.CallerNumberTerminal);
                            Calls.Add(new CallInformation(e.CallerNumberTerminal, e.TargetNumberTerminal, DateTime.Now, CallState.Answered));
                            break;
                        case 'n':
                            Calls.Add(new CallInformation(e.CallerNumberTerminal, e.TargetNumberTerminal, DateTime.Now, CallState.Rejected));
                            target.DropCall();
                            break;
                    }
                }
            }
        }

        private void Answer(object sender, AnswerEventArgs e)
        {
            Console.WriteLine($"Абонент {e.TargetNumberTerminal} ответил на звонок от {e.CallerNumberTerminal}");
            Clients[e.CallerNumberTerminal].Item1.TerminalPort.State = PortState.Busy;
            Clients[e.TargetNumberTerminal].Item1.TerminalPort.State = PortState.Busy;
        }

        private void Drop(object sender, DropEventArgs e)
        {
            var callInformation = Calls.FirstOrDefault(call =>
                call.CallerNumber == e.CallerNumberTerminal || call.TargetNumber == e.CallerNumberTerminal);

            if (callInformation != null && callInformation.EndCall == new DateTime()) 
            {
                int index = Calls.ToList().IndexOf(callInformation);
                Console.WriteLine($"Абонент {e.CallerNumberTerminal} сбросил вызов");
                Calls.ToList()[index].EndCall = DateTime.Now;
                Calls.ToList()[index].Cost =
                    (Calls.ToList()[index].EndCall - Calls.ToList()[index].BeginCall).Seconds * 0.3f;
            }
            
        }
    }
}