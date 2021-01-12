using System;

namespace BillingSystem
{
    public class Record
    {
        private DateTime _date;
        private TimeSpan _callDuration;
        private string _callerNumber;
        private string _targetNumber;
        private float _costCall;

        public Record(TimeSpan callDuration, float costCall, DateTime date, string callerNumber, string targetNumber)
        {
            _callDuration = callDuration;
            _costCall = costCall;
            _date = date;
            _callerNumber = callerNumber;
            _targetNumber = targetNumber;
        }
    }
}