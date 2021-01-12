using System;
using Call.Enums;

namespace Call
{
    public class ActiveCall
    {
        public string CallerNumber { get; }
        public string TargetNumber { get; }
        public DateTime StartTime { get; }
        public DateTime FinishTime { get; private set; }
        public TimeSpan CallTime { get; private set; }
        public CallState CallState { get; set; }
        public double Cost { get; private set; }
        public double CostPerMinute { get; }
        
        public ActiveCall(string callerNumber, string targetNumber, double costPerMinute)
        {
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
            }
            else
            {
                CallTime = TimeSpan.Zero;
            }
        }
    }
}