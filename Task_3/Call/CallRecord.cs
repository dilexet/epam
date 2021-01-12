using System;
using Call.Enums;

namespace Call
{
    public class CallRecord
    {
        private string _terminalNumber;
        private CallType _callType;
        private CallState _callState;
        private DateTime _date;
        private TimeSpan _callDuration;
        private double _costCall;

        public CallRecord(ActiveCall activeCall, CallType callType)
        {
            _callType = callType;
            _callState = activeCall.CallState;
            _date = activeCall.StartTime;
            _callDuration = activeCall.CallTime;
            if (callType == CallType.Incoming)
            {
                _terminalNumber = activeCall.CallerNumber;
                _costCall = 0;
            }
            else
            {
                _terminalNumber = activeCall.TargetNumber;
                _costCall = activeCall.Cost;
            }
        }
    }
}