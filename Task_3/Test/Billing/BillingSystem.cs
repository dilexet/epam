using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Test.ATE;

namespace Test.Billing
{
    public class BillingSystem
    {
        private readonly IDictionary<string, Tuple<Terminal, Contract>> _clients;
        private readonly ICollection<CallInformation> _calls;

        public BillingSystem(ICollection<CallInformation> calls, IDictionary<string, Tuple<Terminal, Contract>> clients)
        {
            _calls = calls;
            _clients = clients;
        }

        public Report CreateReport(string terminalNumber)
        {
            var calls = _calls.Where(
                call => call.CallerNumber == terminalNumber || call.TargetNumber == terminalNumber);
            var contract = _clients[terminalNumber].Item2;
            
            Report report = new Report();
            foreach (var call in calls)
            {
                report.AddRecords(new Record(
                    call.EndCall.Subtract(call.BeginCall),
                    contract.Client,
                    terminalNumber,
                    call.Cost,
                    call.BeginCall));
            }
            return report;
        }
    }
}