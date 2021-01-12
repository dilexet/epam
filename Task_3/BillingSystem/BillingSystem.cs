using System.Collections.Generic;
using Call;

namespace BillingSystem
{
    public delegate void CallReportHandler();
    public class Billing
    {
        public event CallReportHandler CallReportEvent;
        public ICollection<ActiveCall> Calls { get; }

        public Billing()
        {
            Calls = new List<ActiveCall>();
        }

        protected virtual void OnCallReportEvent()
        {
            CallReportEvent?.Invoke();
        }
    }
}