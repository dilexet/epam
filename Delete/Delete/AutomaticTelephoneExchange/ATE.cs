using System;
using System.Collections.Generic;
using System.Linq;
using Delete.Args;
using Delete.BillingSystem;
using Delete.Enums;
using Delete.Interfaces;

namespace Delete.AutomaticTelephoneExchange
{
    public class Ate : IAte
    {
        private IDictionary<int, Tuple<Port, IContract>> _usersData;
        private IList<CallInformation> _callList = new List<CallInformation>();
        public Ate()
        {
            _usersData = new Dictionary<int, Tuple<Port, IContract>>();
            
        }

        public Terminal GetNewTerminal(IContract contract)
        {
            var newPort = new Port();
            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            newPort.EndCallEvent += CallingTo;
            _usersData.Add(contract.Number, new Tuple<Port, IContract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }

        public IContract RegisterContract(Subscriber subscriber, TariffType type)
        {
            var contract = new Contract(subscriber, type);
            return contract;
        }

        public void CallingTo(object sender, ICallingEventArgs e)
        {
            if ((_usersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
                || e is EndCallEventArgs)
            {
                CallInformation inf = null;
                Port targetPort;
                Port port;
                int number = 0;
                int targetNumber = 0; 
                if (e is EndCallEventArgs)
                {
                    var callListFirst = _callList.First(x => x.Id.Equals(e.Id));
                    if (callListFirst.MyNumber == e.TelephoneNumber)
                    {
                        targetPort = _usersData[callListFirst.TargetNumber].Item1;
                        port = _usersData[callListFirst.MyNumber].Item1;
                        number = callListFirst.MyNumber;
                        targetNumber = callListFirst.TargetNumber;
                    }
                    else
                    {
                        port = _usersData[callListFirst.TargetNumber].Item1;
                        targetPort = _usersData[callListFirst.MyNumber].Item1;
                        targetNumber = callListFirst.MyNumber;
                        number = callListFirst.TargetNumber;
                    }
                }
                else
                {
                    targetPort = _usersData[e.TargetTelephoneNumber].Item1;
                    port = _usersData[e.TelephoneNumber].Item1;
                    targetNumber = e.TargetTelephoneNumber;
                    number = e.TelephoneNumber;
                }
                if (targetPort.State == PortState.Connect && port.State == PortState.Connect)
                {
                    var tuple = _usersData[number];
                    var targetTuple = _usersData[targetNumber];

                    if (e is AnswerEventArgs)
                    {
                        
                        var answerArgs = (AnswerEventArgs)e;

                        if (!answerArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(answerArgs.Id)))
                        {
                            inf = _callList.First(x => x.Id.Equals(answerArgs.Id));
                        }

                        if (inf != null)
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall, inf.Id);
                        }
                        else
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                        }
                    }
                    if (e is CallEventArgs)
                    {
                        if (tuple.Item2.Subscriber.Money > tuple.Item2.Tariff.CostOfCallPerMinute)
                        {
                            var callArgs = (CallEventArgs)e;

                            if (callArgs.Id.Equals(Guid.Empty))
                            {
                                inf = new CallInformation(
                                    callArgs.TelephoneNumber,
                                    callArgs.TargetTelephoneNumber, 
                                    DateTime.Now);
                                _callList.Add(inf);
                            }

                            if (!callArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(callArgs.Id)))
                            {
                                inf = _callList.First(x => x.Id.Equals(callArgs.Id));
                            }
                            if (inf != null)
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, inf.Id);
                            }
                            else
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Terminal with number {0} is not enough money in the account!", e.TelephoneNumber);

                        }
                    }
                    if (e is EndCallEventArgs)
                    {
                        var args = (EndCallEventArgs)e;
                        inf = _callList.First(x => x.Id.Equals(args.Id));
                        inf.EndCall = DateTime.Now;
                        var sumOfCall = tuple.Item2.Tariff.CostOfCallPerMinute * TimeSpan.FromTicks((inf.EndCall - inf.BeginCall).Ticks).TotalMinutes;
                        inf.Cost = (int)sumOfCall;
                        targetTuple.Item2.Subscriber.RemoveMoney(inf.Cost);
                        targetPort.AnswerCall(args.TelephoneNumber, args.TargetTelephoneNumber, CallState.Rejected, inf.Id);
                    }
                }
            }
            else if (!_usersData.ContainsKey(e.TargetTelephoneNumber))
            {
                Console.WriteLine("You have calling a non-existent number!!!");
            }
            else
            {
                Console.WriteLine("You have calling a your number!!!");
            }
        }

        public IList<CallInformation> GetInfoList()
        {
            return _callList;
        }
    }
}
