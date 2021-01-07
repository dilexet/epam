using Test.Enums;

namespace Test.ATE
{
    public class Port
    {
        public PortState State { get; private set; }

        public Port()
        {
            State = PortState.Off;
        }

        public void Connect()
        {
            State = PortState.Free;
        }

        public void Disconnect()
        {
            State = PortState.Off;
        }
    }
}