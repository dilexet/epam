using AutomaticTelephoneStation.Enums;

namespace AutomaticTelephoneStation.ATS
{
    public delegate void ConnectHandler();
    public delegate void DisconnectHandler();
    
    public class Port
    {
        public PortState State { get; private set; }
        
        public Port()
        {
            State = PortState.Off;
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