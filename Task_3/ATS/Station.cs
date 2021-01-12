using System;
using System.Collections;
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
        private readonly ICollection<Contract> _contracts;
        private readonly ICollection<ActiveCall> _activeCalls;
        public Station()
        {
            _contracts = new List<Contract>();
            _activeCalls = new List<ActiveCall>();
        }

        public Contract ConcludeContract(Client client, TariffType tariffType)
        {
            var contract = new Contract(client, tariffType);
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
        
        // TODO: додумать как использовать состояние звонка ОЖИДАНИЕ, если звонок на ожидании то ни caller-у ни target-у никто не сможет довзониться
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
                  
                    // TODO: стоимость звонка расчитывать иходя из тарифа
                    _activeCalls.Add(new ActiveCall(e.CallerNumberTerminal, e.TargetNumberTerminal, 3));
                }
            }
        }

        private void AnswerHandler(object sender, AnswerEventArgs e)
        {
            var activeCall = _activeCalls.FirstOrDefault(call =>
                call.CallerNumber == e.TargetNumberTerminal || call.TargetNumber == e.TargetNumberTerminal);
            if (activeCall != null)
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
                string callerNumber = activeCall.CallerNumber;
                string targetNumber = activeCall.TargetNumber;

                var caller = _contracts.First(contract => contract.Terminal.TerminalNumber == callerNumber);
                var target = _contracts.First(contract => contract.Terminal.TerminalNumber == targetNumber);
               
                caller.Terminal.EndCall();
                //TODO: куда-то должны передаваться два контракта, активный звонок, и CallType
                _activeCalls.Remove(activeCall);
            }
        }
    }
}