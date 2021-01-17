using AutomaticTelephoneStation.ATS.Enums;
using AutomaticTelephoneStation.ATS.Interfaces;

namespace AutomaticTelephoneStation.ATS
{
    public class Port : IPort
    {
        public string Number { get; }
        public PortState State { get; private set; }

        public Port(string number)
        {
            State = PortState.Off;
            Number = number;
        }

        public void Connect()
        {
            if (State == PortState.Off)
            {
                State = PortState.Free;
            }
        }

        public void Disconnect()
        {
            if (State == PortState.Free)
            {
                State = PortState.Off;
            }
        }

        public void Call()
        {
            if (State == PortState.Free)
            {
                State = PortState.Busy;
            }
        }

        public void EndCall()
        {
            if (State == PortState.Busy)
            {
                State = PortState.Free;
            }
        }
    }
}