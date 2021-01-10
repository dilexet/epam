using System;

namespace Test.ATE
{
    public class CallInformation
    {
        public CallInformation(string callerNumber, string targetNumber, DateTime beginCall)
        {
            CallerNumber = callerNumber;
            TargetNumber = targetNumber;
            BeginCall = beginCall;
        }
        public string CallerNumber { get; }
        public string TargetNumber { get; }
        public DateTime BeginCall { get; }
        public DateTime EndCall { get; set; }
        // public int Cost { get; }
    }
}