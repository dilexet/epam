using System;
using System.Collections.Generic;
using AutomaticTelephoneStation.ATS.Interfaces;
using AutomaticTelephoneStation.BillingSystem;

namespace AutomaticTelephoneStation.ATS
{
    public class Company
    {
        public event EventHandler<Contract> ConcludeContractEvent;
        private readonly ICollection<IPort> _ports;

        public Company()
        {
            _ports = new List<IPort>();
        }

        public Contract ConcludeContract(Client client, Tariff tariff, string number)
        {
            TerminalBuilder terminalBuilder = new TerminalBuilder(number);
            var contract = new Contract(client, tariff) {Terminal = terminalBuilder.GetNewTerminal()};
            _ports.Add(contract.Terminal.TerminalPort);
            ConcludeContractEvent?.Invoke(this, contract);
            return contract;
        }

        public ICollection<IPort> GetPorts()
        {
            return _ports;
        }
    }
}