using System;

namespace Test.Billing
{
    public class Record
    {
        private DateTime _date;
        private TimeSpan _callDuration;
        private Client _client;
        private string _terminalNumber;
        private float _costCall;

        public Record(TimeSpan callDuration, Client client, string terminalNumber, float costCall, DateTime date)
        {
            _callDuration = callDuration;
            _client = client;
            _terminalNumber = terminalNumber;
            _costCall = costCall;
            _date = date;
        }
    }
}