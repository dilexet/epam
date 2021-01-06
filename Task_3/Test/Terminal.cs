using Test.EventArgs;

namespace Test
{
    public class Terminal
    {
        public string TerminalNumber { get; }
        private readonly Port _port;

        public delegate void CallHandler(object sender, CallEventArgs e);
        public event CallHandler CallEvent;

        public delegate void AnswerHandler(object sender, AnswerEventArgs e);
        public event AnswerHandler AnswerEvent;
        
        public delegate void DropHandler(object sender, DropEventsArgs e);
        public event DropHandler DropEvent;
        
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
        
        public void Call(string targetNumberTerminal)
        {
            CallEvent?.Invoke(this, new CallEventArgs(TerminalNumber, targetNumberTerminal));
        }
        
        public void Answer(string targetNumberTerminal)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(TerminalNumber, targetNumberTerminal));
        }

        public void Drop(string targetNumberTerminal)
        {
            DropEvent?.Invoke(this, new DropEventsArgs(TerminalNumber, targetNumberTerminal));
        }

        
    }
}