using System;
using AutomaticTelephoneStation.ATS;
using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.BillingSystem.Enums;

namespace AutomaticTelephoneStation.BillingSystem
{
    public class CallRecord
    {
        public string Number { get; }
        public CallType CallType { get; }
        public CallState CallState { get; }
        public DateTime Date { get; }
        public DateTime FinishTime { get; }
        public TimeSpan CallDuration { get; }
        public double CostCall { get; set; }

        public CallRecord(ActiveCall activeCall, CallType callType)
        {
            CallType = callType;
            CallState = activeCall.CallState;
            Date = activeCall.StartTime;
            FinishTime = DateTime.Now;
            Number = callType == CallType.Incoming ? activeCall.TargetNumber : activeCall.CallerNumber;
            
            if (CallState == CallState.Answered)
            {
                CallDuration = FinishTime - Date;
            }
            else
            {
                CallState = CallState.Rejected;
                CallDuration = TimeSpan.Zero;
            }
            
        }

        public override string ToString()
        {
            return $"Terminal number: {Number}|\n" +
                   $"Call type: {CallType}|\n" +
                   $"CallState: {CallState}|\n" +
                   $"Date: {Date}|\n" +
                   $"Call duration: {CallDuration:g}|\n" +
                   $"Cost call: {CostCall}";
        }
    }
}