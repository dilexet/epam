using System.Collections.Generic;
using System.Linq;

namespace AutomaticTelephoneStation.BillingSystem.Report
{
    public static class ReportRender
    {
        public static IEnumerable<CallRecord> FilterTerminalNumber(CallReport callReport, string terminalNumber)
        {
            return callReport.GetRecords().Where(call => call.TerminalNumber == terminalNumber);
        }
        public static IEnumerable<CallRecord> FilterCallDate(CallReport callReport)
        {
            return callReport.GetRecords().OrderBy(call => call.Date);
        }
        public static IEnumerable<CallRecord> FilterCallCost(CallReport callReport)
        {
            return callReport.GetRecords().OrderBy(call => call.CostCall);
        }
    }
}