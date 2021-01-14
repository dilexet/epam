using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.ATS.EventArgs;
using AutomaticTelephoneStation.BillingSystem;
using AutomaticTelephoneStation.BillingSystem.Enums;
using AutomaticTelephoneStation.BillingSystem.Report;

namespace AutomaticTelephoneStation.ATS
{
    public class Station
    {
        private readonly ICollection<Contract> _contracts;
        private readonly ICollection<ActiveCall> _activeCalls;
        public event EventHandler<CallRecord> CallReportEvent;
        
        public Station()
        {
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
            
            terminal.CallEvent += Call;
            terminal.AnswerEvent += Answer;
            terminal.DropEvent += Drop;
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
        
        private void Call(object sender, CallEventArgs e)
        {
            var caller = _contracts.First(contract => 
                contract.Terminal.TerminalNumber == e.CallerNumberTerminal);
            var target = _contracts.First(contract => 
                contract.Terminal.TerminalNumber == e.TargetNumberTerminal);

            if (caller != null && target != null)
            {
                if (caller.Client.Money <= 0)
                {
                    Console.WriteLine("На вашем счёте недостаточно средств");
                }
                else if (target.Terminal.TerminalPort.State != PortState.Free)
                {
                    Console.WriteLine("Вызываемый абонент занят или отключен");
                    caller.Terminal.DropCall();
                }
                else
                {
                    Console.WriteLine(
                        $"Входящий вызов на номер {e.TargetNumberTerminal} с терминала с номером {e.CallerNumberTerminal}");
                    target.Terminal.IncomingCall();
                    _activeCalls.Add(new ActiveCall(caller.Client, e.CallerNumberTerminal, e.TargetNumberTerminal, caller.Tariff.CostPerMinute));
                }
            }
        }

        private void Answer(object sender, AnswerEventArgs e)
        {
            var activeCall = _activeCalls.FirstOrDefault(call =>
                call.CallerNumber == e.TargetNumberTerminal || call.TargetNumber == e.TargetNumberTerminal);
            if (activeCall != null && activeCall.TargetNumber == e.TargetNumberTerminal) 
            {
                activeCall.CallState = CallState.Answered;
                Console.WriteLine($"Абонент {activeCall.TargetNumber} ответил на звонок от {activeCall.CallerNumber}");
            }
        }
        
        private void Drop(object sender, DropEventArgs e)
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
                // TODO: допилить списание средств
                activeCall.End();
                Console.WriteLine($"Звонок между {activeCall.CallerNumber} и {activeCall.TargetNumber} завершён");
                
                OnCallReport(this, new CallRecord(activeCall, CallType.Incoming));
                OnCallReport(this, new CallRecord(activeCall, CallType.Outgoing));
                _activeCalls.Remove(activeCall);
            }
        }

        protected virtual void OnCallReport(object sender, CallRecord callRecord)
        {
            CallReportEvent?.Invoke(sender, callRecord);
        }
    }
}