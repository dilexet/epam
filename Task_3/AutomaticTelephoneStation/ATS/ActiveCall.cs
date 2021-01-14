using System;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.BillingSystem;

namespace AutomaticTelephoneStation.ATS
{
    public class ActiveCall
    {
        private readonly Client _caller;
        public string CallerNumber { get; }
        public string TargetNumber { get; }
        public DateTime StartTime { get; }
        public DateTime FinishTime { get; private set; }
        public TimeSpan CallTime { get; private set; }
        public CallState CallState { get; set; }
        public double Cost { get; private set; }
        public double CostPerMinute { get; }
        
        public ActiveCall(Client caller, string callerNumber, string targetNumber, double costPerMinute)
        {
            _caller = caller;
            CallerNumber = callerNumber;
            TargetNumber = targetNumber;
            CostPerMinute = costPerMinute;
            StartTime = DateTime.Now;
            CallState = CallState.Expected;
        }
        
        public void End()
        {
            FinishTime = DateTime.Now;
            if (CallState == CallState.Answered)
            {
                CallTime = FinishTime - StartTime;
                Cost = (CallTime.Minutes + 1) * CostPerMinute;
                _caller.RemoveMoney(Cost);
            }
            else
            {
                CallState = CallState.Rejected;
                CallTime = TimeSpan.Zero;
            }
        }
    }
}