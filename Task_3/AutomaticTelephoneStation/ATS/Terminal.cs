using System;
using AutomaticTelephoneStation.ATS.EventArgs;

namespace AutomaticTelephoneStation.ATS
{
    public class Terminal
    {
        public string TerminalNumber { get; }
        public Port TerminalPort { get; }
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;
        public event EventHandler<DropEventArgs> DropEvent;
        public event Action ConnectEvent;
        public event Action DisconnectEvent;
        
        public Terminal(string terminalNumber, Port port)
        {
            TerminalNumber = terminalNumber;
            TerminalPort = port;
            ConnectEvent += TerminalPort.с_Connect;
            DisconnectEvent += TerminalPort.с_Disconnect;
        }

        public void ConnectToPort()
        {
            OnConnectToPort();
        }
        
        public void DisconnectFromPort()
        {
            OnDisconnectFromPort();
        }
        
        public void CallTo(string targetNumberTerminal)
        {
            TerminalPort.Call();
            OnMakeCall(targetNumberTerminal);
        }

        public void AnswerToCall()
        {
            TerminalPort.Call();
            OnAnswerCall();
        }

        public void DropCall()
        {
            TerminalPort.EndCall();
            OnDropCall();
        }
        
        protected virtual void OnConnectToPort()
        {
            ConnectEvent?.Invoke();
        }

        protected virtual void OnDisconnectFromPort()
        {
            DisconnectEvent?.Invoke();
        }

        protected virtual void OnMakeCall(string targetNumberTerminal)
        {
            CallEvent?.Invoke(this, new CallEventArgs(TerminalNumber, targetNumberTerminal));
        }
        
        protected virtual void OnAnswerCall()
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(TerminalNumber));
        }

        protected virtual void OnDropCall()
        {
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