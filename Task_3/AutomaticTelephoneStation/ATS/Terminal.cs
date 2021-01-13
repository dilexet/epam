using AutomaticTelephoneStation.ATS.EventArgs;

namespace AutomaticTelephoneStation.ATS
{
    public delegate void CallHandler(object sender, CallEventArgs e);
    public delegate void AnswerHandler(object sender, AnswerEventArgs e);
    public delegate void DropHandler(object sender, DropEventArgs e);
    
    public class Terminal
    {
        public string TerminalNumber { get; }
        public Port TerminalPort { get; }
        
        public event CallHandler CallEvent;
        public event AnswerHandler AnswerEvent;
        public event DropHandler DropEvent;
        public event ConnectHandler ConnectEvent;
        public event DisconnectHandler DisconnectEvent;
        
        public Terminal(string terminalNumber, Port port)
        {
            TerminalNumber = terminalNumber;
            TerminalPort = port;
            ConnectEvent += TerminalPort.Connect;
            DisconnectEvent += TerminalPort.Disconnect;
        }

        public virtual void ConnectToPort()
        {
            ConnectEvent?.Invoke();
        }

        public virtual void DisconnectFromPort()
        {
            DisconnectEvent?.Invoke();
        }

        public virtual void CallTo(string targetNumberTerminal)
        {
            TerminalPort.Call();
            CallEvent?.Invoke(this, new CallEventArgs(TerminalNumber, targetNumberTerminal));
        }
        
        public virtual void AnswerToCall()
        {
            TerminalPort.Call();
            AnswerEvent?.Invoke(this, new AnswerEventArgs(TerminalNumber));
        }

        public virtual void DropCall()
        {
            TerminalPort.EndCall();
            DropEvent?.Invoke(this, new DropEventArgs(TerminalNumber));
        }

        public void IncomingCall()
        {
            TerminalPort.Call();
        }
        public void EndCall()
        {
            TerminalPort.EndCall();
        }
    }
}