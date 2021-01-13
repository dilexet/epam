using System;
using AutomaticTelephoneStation.ATS;
using AutomaticTelephoneStation.Enums;

namespace AutomaticTelephoneStation.BillingSystem.Report
{
    public class CallRecord
    {
        public string TerminalNumber { get; }
        private CallType CallType { get; }
        private CallState CallState { get; }
        public DateTime Date { get; }
        private TimeSpan CallDuration { get; }
        public double CostCall { get; }

        public CallRecord(ActiveCall activeCall, CallType callType)
        {
            CallType = callType;
            CallState = activeCall.CallState;
            Date = activeCall.StartTime;
            CallDuration = activeCall.CallTime;
            if (callType == CallType.Incoming)
            {
                TerminalNumber = activeCall.TargetNumber;
                CostCall = 0;
            }
            else
            {
                TerminalNumber = activeCall.CallerNumber;
                CostCall = activeCall.Cost;
            }
        }

        public override string ToString()
        {
            return $"Terminal number: {TerminalNumber}|\n" +
                   $"Call type: {CallType}|\n" +
                   $"CallState: {CallState}|\n" +
                   $"Date: {Date}|\n" +
                   $"Call duration: {CallDuration:g}|\n" +
                   $"Cost call: {CostCall}";
        }
    }
}