using Test.EventArgs;

namespace Test.ATE
{
    public class Terminal
    {
        public string TerminalNumber { get; }
        public Port TerminalPort { get; }
        
        public delegate void CallHandler(object sender, CallEventArgs e);
        public event CallHandler CallEvent;

        public delegate void AnswerHandler(object sender, AnswerEventArgs e);
        public event AnswerHandler AnswerEvent;
        
        public delegate void DropHandler(object sender, DropEventArgs e);
        public event DropHandler DropEvent;
        public Terminal(string terminalNumber, Port port)
        {
            TerminalNumber = terminalNumber;
            TerminalPort = port;
        }

        public void ConnectToPort()
        {
            TerminalPort.Connect();
        }

        public void DisconnectFromPort()
        {
            TerminalPort.Disconnect();
        }

        public void CallTo(string targetNumberTerminal)
        {
            CallEvent?.Invoke(this, new CallEventArgs(TerminalNumber, targetNumberTerminal));
        }
        
        public void AnswerToCall(string targetNumberTerminal)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(TerminalNumber, targetNumberTerminal));
        }

        public void DropCall()
        {
            DropEvent?.Invoke(this, new DropEventArgs(TerminalNumber));
        }
    }
}