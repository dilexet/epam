using System.Collections.Generic;
using Call;

namespace BillingSystem
{
    public delegate void CallReportHandler();
    public class Billing
    {
        public event CallReportHandler CallReportEvent;
        public ICollection<CallInformation> Calls { get; }

        public Billing()
        {
            Calls = new List<CallInformation>();
        }

        protected virtual void OnCallReportEvent()
        {
            CallReportEvent?.Invoke();
        }
    }
}