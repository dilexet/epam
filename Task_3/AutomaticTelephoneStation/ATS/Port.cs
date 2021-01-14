using AutomaticTelephoneStation.ATS.Enums;

namespace AutomaticTelephoneStation.ATS
{
    public class Port
    {
        public PortState State { get; private set; }
        
        public Port()
        {
            State = PortState.Off;
        }

        public void с_Connect()
        {
            if (State == PortState.Off)
            {
                State = PortState.Free;
            }
        }

        public void с_Disconnect()
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