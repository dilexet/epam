using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.ATS.EventArgs;
using AutomaticTelephoneStation.ATS.Interfaces;
using AutomaticTelephoneStation.BillingSystem;
using AutomaticTelephoneStation.BillingSystem.Enums;

namespace AutomaticTelephoneStation.ATS
{
    public class TelephoneStation : ITelephoneStation
    {
        private readonly Company _company;
        private readonly ICollection<ActiveCall> _activeCalls;
        public event EventHandler<CallRecord> CallEndedEvent;
        
        public TelephoneStation(Company company)
        {
            _company = company;
            _activeCalls = new List<ActiveCall>();
        }

        public void CallHandler(object sender, System.EventArgs e)
        {
            if (e is CallEventArgs args)
            {
                var caller = _company.GetPorts().FirstOrDefault(port => 
                    port.Number == args.CallerNumberTerminal);
                var target = _company.GetPorts().FirstOrDefault(port => 
                    port.Number == args.TargetNumberTerminal);

                // TODO: обработка исключений
                
                if (caller != null && target != null)
                {
                    if (target.State != PortState.Free)
                    {
                        Console.WriteLine("Вызываемый абонент занят или отключен");
                        caller.EndCall();
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Входящий вызов на номер {args.TargetNumberTerminal} с терминала с номером {args.CallerNumberTerminal}");
                        target.Call();

                        _activeCalls.Add(new ActiveCall(args.CallerNumberTerminal, args.TargetNumberTerminal)); 
                    }
                }
            }
        }

        public void AnswerHandler(object sender, System.EventArgs e)
        {
            if (e is AnswerEventArgs args)
            {
                var activeCall = _activeCalls.FirstOrDefault(call =>
                    call.CallerNumber == args.TargetNumberTerminal || call.TargetNumber == args.TargetNumberTerminal);
                if (activeCall != null && activeCall.TargetNumber == args.TargetNumberTerminal)
                {
                    activeCall.CallState = CallState.Answered;
                    Console.WriteLine(
                        $"Абонент {activeCall.TargetNumber} ответил на звонок от {activeCall.CallerNumber}");
                }
            }
        }
        
        public void DropHandler(object sender, System.EventArgs e)
        {
            if (e is DropEventArgs args)
            {
                var activeCall = _activeCalls.FirstOrDefault(call =>
                    call.CallerNumber == args.CallerNumberTerminal || call.TargetNumber == args.CallerNumberTerminal);

                if (activeCall != null)
                {
                    if (activeCall.CallerNumber == args.CallerNumberTerminal)
                    {
                        var target = _company.GetPorts().FirstOrDefault(port => port.Number == activeCall.TargetNumber);
                        target?.EndCall();
                    }
                    else if (activeCall.TargetNumber == args.CallerNumberTerminal)
                    {
                        var caller = _company.GetPorts().FirstOrDefault(port => port.Number == activeCall.CallerNumber);
                        caller?.EndCall();
                    }
                    
                    Console.WriteLine($"Звонок между {activeCall.CallerNumber} и {activeCall.TargetNumber} завершён");

                    OnCallReport(this, new CallRecord(activeCall, CallType.Incoming));
                    OnCallReport(this, new CallRecord(activeCall, CallType.Outgoing));
                    _activeCalls.Remove(activeCall);
                }
            }
        }

        protected virtual void OnCallReport(object sender, CallRecord callRecord)
        {
            CallEndedEvent?.Invoke(sender, callRecord);
        }
    }
}