using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.ATS.EventArgs;
using AutomaticTelephoneStation.BillingSystem;
using AutomaticTelephoneStation.Enums;

namespace AutomaticTelephoneStation.ATS
{
    public class Station
    {
        private readonly ICollection<Contract> _contracts;
        private readonly ICollection<ActiveCall> _activeCalls;
        public event CallReportHandler CallReportEvent;
        public Station(Billing billing)
        {
            CallReportEvent += billing.CallReportHandler;
            _contracts = new List<Contract>();
            _activeCalls = new List<ActiveCall>();
        }

        public Contract ConcludeContract(Client client, Tariff tariff)
        {
            var contract = new Contract(client, tariff);
            contract.Terminal = GetNewTerminal();
            _contracts.Add(contract);
            return contract;
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
            } while (_contracts.FirstOrDefault(contract => contract.Terminal.TerminalNumber == number) != null);
            return number;
        }
        
        private void CallHandler(object sender, CallEventArgs e)
        {
            var caller = _contracts.First(contract => 
                contract.Terminal.TerminalNumber == e.CallerNumberTerminal);
            var target = _contracts.First(contract => 
                contract.Terminal.TerminalNumber == e.TargetNumberTerminal);

            if (caller != null && target != null)
            {
                if (target.Terminal.TerminalPort.State != PortState.Free)
                {
                    Console.WriteLine("Вызываемый абонент занят или отключен");
                    caller.Terminal.DropCall();
                    //TODO: куда-то должны передаваться два контракта, активный звонок, и CallType
                }
                else
                {
                    Console.WriteLine(
                        $"Входящий вызов на номер {e.TargetNumberTerminal} с терминала с номером {e.CallerNumberTerminal}");
                    target.Terminal.IncomingCall();
                    _activeCalls.Add(new ActiveCall(e.CallerNumberTerminal, e.TargetNumberTerminal, caller.Tariff.CostPerMinute));
                }
            }
        }

        private void AnswerHandler(object sender, AnswerEventArgs e)
        {
            var activeCall = _activeCalls.FirstOrDefault(call =>
                call.CallerNumber == e.TargetNumberTerminal || call.TargetNumber == e.TargetNumberTerminal);
            if (activeCall != null && activeCall.TargetNumber == e.TargetNumberTerminal) 
            {
                activeCall.CallState = CallState.Answered;
                Console.WriteLine($"Абонент {activeCall.TargetNumber} ответил на звонок от {activeCall.CallerNumber}");
            }
        }
        
        private void DropHandler(object sender, DropEventArgs e)
        {
            var activeCall = _activeCalls.FirstOrDefault(call =>
                call.CallerNumber == e.CallerNumberTerminal || call.TargetNumber == e.CallerNumberTerminal);

            if (activeCall != null)
            {
                
                if (activeCall.CallerNumber == e.CallerNumberTerminal)
                {
                    var target = _contracts.First(contract => contract.Terminal.TerminalNumber == activeCall.TargetNumber);
                    target.Terminal.EndCall();
                }
                else if (activeCall.TargetNumber == e.CallerNumberTerminal)
                {
                    var caller = _contracts.First(contract => contract.Terminal.TerminalNumber == activeCall.CallerNumber);
                    caller.Terminal.EndCall();
                }
                
                activeCall.End();
                Console.WriteLine($"Звонок между {activeCall.CallerNumber} и {activeCall.TargetNumber} завершён");
                CallReportEvent?.Invoke(activeCall, CallType.Incoming);
                CallReportEvent?.Invoke(activeCall, CallType.Outgoing);
                _activeCalls.Remove(activeCall);
            }
        }
    }
}