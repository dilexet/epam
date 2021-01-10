using Test.EventArgs;

namespace Test.ATE
{
    public class Terminal
    {
        public string TerminalNumber { get; }
        public Port TerminalPort { get; set; }
        
        public delegate void CallHandler(object sender, CallEventArgs e);
        public event CallHandler CallEvent;

        public delegate void AnswerHandler(object sender, AnswerEventArgs e);
        public event AnswerHandler AnswerEvent;
        
        public delegate void DropHandler(object sender, DropEventArgs e);
        public event DropHandler DropEvent;

        public delegate void ConnectHandler(Terminal terminal);
        public event ConnectHandler ConnectEvent;
        
        public delegate void DisconnectHandler(Terminal terminal);
        public event DisconnectHandler DisconnectEvent;
        
        public Terminal(string terminalNumber, Port port)
        {
            TerminalNumber = terminalNumber;
            TerminalPort = port;
        }

        public void ConnectToPort()
        {
            ConnectEvent?.Invoke(this);
        }

        public void DisconnectFromPort()
        {
            DisconnectEvent?.Invoke(this);
        }

        public void CallTo(string targetNumberTerminal)
        {
            CallEvent?.Invoke(this, new CallEventArgs(TerminalNumber, targetNumberTerminal));
        }
        
        public void AnswerToCall(string callerNumberTerminal)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(callerNumberTerminal, TerminalNumber));
        }

        public void DropCall()
        {
            DropEvent?.Invoke(this, new DropEventArgs(TerminalNumber));
        }
    }
}