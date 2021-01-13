using AutomaticTelephoneStation.ATS;
using AutomaticTelephoneStation.BillingSystem.Report;
using AutomaticTelephoneStation.Enums;

namespace AutomaticTelephoneStation.BillingSystem
{
    public delegate void CallReportHandler(ActiveCall activeCall, CallType callType);
    public class Billing
    {
        private readonly CallReport _callReport;

        public Billing()
        {
            _callReport = new CallReport();
        }

        public void CallReportHandler(ActiveCall activeCall, CallType callType)
        {
            _callReport.AddRecords(new CallRecord(activeCall, callType));
        }

        public CallReport GetReport()
        {
            return _callReport;
        }
    }
}