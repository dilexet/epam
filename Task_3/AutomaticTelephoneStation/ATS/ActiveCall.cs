using System;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.BillingSystem;

namespace AutomaticTelephoneStation.ATS
{
    public class ActiveCall
    {
        public string CallerNumber { get; }
        public string TargetNumber { get; }
        public DateTime StartTime { get; }
        public CallState CallState { get; set; }

        public ActiveCall(string callerNumber, string targetNumber)
        {
            CallerNumber = callerNumber;
            TargetNumber = targetNumber;
            StartTime = DateTime.Now;
            CallState = CallState.Expected;
        }
    }
}