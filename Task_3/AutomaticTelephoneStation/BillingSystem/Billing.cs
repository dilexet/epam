using AutomaticTelephoneStation.BillingSystem.Report;

namespace AutomaticTelephoneStation.BillingSystem
{
    public class Billing
    {
        private readonly CallReport _callReport;

        public Billing()
        {
            _callReport = new CallReport();
        }

        public void c_CallReport(object sender, CallRecord callRecord)
        {
            _callReport.AddRecords(callRecord);
        }

        public CallReport GetReport()
        {
            return _callReport;
        }
    }
}