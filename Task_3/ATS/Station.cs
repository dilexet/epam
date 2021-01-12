using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Enums;
using ATS.EventArgs;
using BillingSystem;
using Call;
using Call.Enums;

namespace ATS
{
    public class Station
    {
        public ICollection<Contract> Contracts { get; }
        private readonly Billing _billing;
        public Station(Billing billing)
        {
            Contracts = new List<Contract>();
            _billing = billing;
        }

        public void ConcludeContract(Client client, TariffType tariffType)
        {
            var contract = new Contract(client, tariffType);
            contract.Client.Terminal = GetNewTerminal();
            Contracts.Add(contract);
        }

        private Terminal GetNewTerminal()
        {
            Port port = new Port();
            string terminalNumber = GetTerminalNumber();
            Terminal terminal = new Terminal(terminalNumber, port);
            
            terminal.CallEvent += CallHandler;
            terminal.AnswerEvent += AnswerHandler;
            terminal.DropEvent += DropHandler;

            terminal.ConnectEvent += terminal.TerminalPort.Connect;
            terminal.DisconnectEvent += terminal.TerminalPort.Disconnect;
            return terminal;
        }
        
        private string GetTerminalNumber()
        {
            Random random = new Random();
            string number;
            do
            {
                number = $"{random.Next(1000, 9999)}{random.Next(1000, 9999)}";
            } while (Contracts.FirstOrDefault(contract => contract.Client.Terminal.TerminalNumber == number) != null);
            return number;
        }
        

        private void CallHandler(object sender, CallEventArgs e)
        {
            var caller = Contracts.First(contract => 
                contract.Client.Terminal.TerminalNumber == e.CallerNumberTerminal);
            var target = Contracts.First(contract => 
                contract.Client.Terminal.TerminalNumber == e.TargetNumberTerminal);

            if (caller != null && target != null)
            {
                if (target.Client.Terminal.TerminalPort.State != PortState.Free)
                {
                    Console.WriteLine("Вызываемый абонент занят или отключен");
                    target.Client.Terminal.DropCall();
                }
                else
                {
                    Console.WriteLine(
                        $"Входящий вызов на номер {e.TargetNumberTerminal} с терминала с номером {e.CallerNumberTerminal}");
                    Console.WriteLine("Ответить? y/n");
                    char k = Console.ReadKey(true).KeyChar;
                    CallInformation callInformation = new CallInformation(e.CallerNumberTerminal, e.TargetNumberTerminal, DateTime.Now);
                    switch (k)
                    {
                        case 'y':
                            target.Client.Terminal.AnswerToCall(e.CallerNumberTerminal);
                            callInformation.CallState = CallState.Answered;
                            break;
                        case 'n':
                            target.Client.Terminal.DropCall();
                            callInformation.CallState = CallState.Rejected;
                            break;
                    }

                    _billing.Calls.Add(callInformation);
                }
            }
        }

        private void AnswerHandler(object sender, AnswerEventArgs e)
        {
            Console.WriteLine($"Абонент {e.TargetNumberTerminal} ответил на звонок от {e.CallerNumberTerminal}");
        }
        
        private void DropHandler(object sender, DropEventArgs e)
        {
            var callInformation = _billing.Calls.FirstOrDefault(call =>
                call.CallerNumber == e.CallerNumberTerminal || call.TargetNumber == e.CallerNumberTerminal);

            if (callInformation != null && callInformation.EndCall == new DateTime()) 
            {
                Console.WriteLine($"Абонент {e.CallerNumberTerminal} сбросил вызов");
                var caller = Contracts.First(contract =>
                    contract.Client.Terminal.TerminalNumber != e.CallerNumberTerminal && (
                        contract.Client.Terminal.TerminalNumber == callInformation.CallerNumber ||
                        contract.Client.Terminal.TerminalNumber == callInformation.TargetNumber));
                caller.Client.Terminal.EndCall();
               
                int index = _billing.Calls.ToList().IndexOf(callInformation);
                _billing.Calls.ToList()[index].EndCall = DateTime.Now;
                _billing.Calls.ToList()[index].Cost =
                    (_billing.Calls.ToList()[index].EndCall - _billing.Calls.ToList()[index].BeginCall).Seconds * 0.3f;
            }
        }
    }
}