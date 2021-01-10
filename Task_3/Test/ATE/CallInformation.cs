using System;
using System.Runtime.CompilerServices;
using Test.Enums;

namespace Test.ATE
{
    public class CallInformation
    {
        public CallInformation(string callerNumber, string targetNumber, DateTime beginCall, CallState callState)
        {
            CallerNumber = callerNumber;
            TargetNumber = targetNumber;
            BeginCall = beginCall;
            CallState = callState;
        }
        public string CallerNumber { get; }
        public string TargetNumber { get; }
        public DateTime BeginCall { get; }
        public DateTime EndCall { get; set; }
        public CallState CallState { get; }
        public float Cost { get; set; }
    }
}