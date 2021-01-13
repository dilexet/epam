using System.Collections.Generic;

namespace AutomaticTelephoneStation.BillingSystem.Report
{
    public class CallReport
    {
        private ICollection<CallRecord> _records;
        public CallReport()
        {
            _records = new List<CallRecord>();
        }

        public void AddRecords(CallRecord record)
        {
            _records.Add(record);
        }
        public ICollection<CallRecord> GetRecords()
        {
            return _records;
        }
    }
}