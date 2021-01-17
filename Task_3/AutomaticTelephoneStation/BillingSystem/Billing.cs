using System.Collections.Generic;
using System.Linq;
using AutomaticTelephoneStation.BillingSystem.Enums;

namespace AutomaticTelephoneStation.BillingSystem
{
    public class Billing
    {
        private readonly ICollection<CallRecord> _records;
        private readonly ICollection<Contract> _contracts;
        
        public Billing()
        {
            _records = new List<CallRecord>();
            _contracts = new List<Contract>();
        }

        public void CallEndedHandler(object sender, CallRecord callRecord)
        {
            var contract = _contracts.FirstOrDefault(c => c.Terminal.TerminalPort.Number == callRecord.Number);
            if (contract != null)
            {
                callRecord.CostCall = callRecord.CallType == CallType.Outgoing
                    ? (callRecord.CallDuration.Minutes + 1) * contract.Tariff.CostPerMinute
                    : 0;
            }
            _records.Add(callRecord);
        }

        public void ConcludeContractHandler(object sender, Contract contract)
        {
            _contracts.Add(contract);
        }
        public IEnumerable<CallRecord> FilterNumber(string number)
        {
            return _records.Where(call => call.Number == number);
        }
        
        public IEnumerable<CallRecord> FilterCallDate(string number)
        {
            return FilterNumber(number).OrderBy(call => call.Date);
        }
        
        public IEnumerable<CallRecord> FilterCallCost(string number)
        {
            return FilterNumber(number).OrderBy(call => call.CostCall);
        }
        
    }
}