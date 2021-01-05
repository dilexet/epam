namespace Test
{
    public class Terminal
    {
        public string TerminalNumber { get; }
        private Port _port;

        public Terminal(string terminalNumber, Port port)
        {
            TerminalNumber = terminalNumber;
            _port = port;
        }

        public void ConnectToPort()
        {
            _port.Connect();
        }
        public void DisconnectFromPort()
        {
            _port.Disconnect();
        }
        public void Call()
        {
            
        }

        public void Drop()
        {
            
        }

        public void Answer()
        {
            
        }
    }
}