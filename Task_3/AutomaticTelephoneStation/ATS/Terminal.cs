using System;
using AutomaticTelephoneStation.ATS.EventArgs;
using AutomaticTelephoneStation.ATS.Interfaces;

namespace AutomaticTelephoneStation.ATS
{
    public class Terminal
    {
        public IPort TerminalPort { get; }
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;
        public event EventHandler<DropEventArgs> DropEvent;
        public event Action ConnectEvent;
        public event Action DisconnectEvent;
        
        public Terminal(IPort port)
        {
            TerminalPort = port;
        }

        public void ConnectToPort()
        {
            ConnectEvent += TerminalPort.Connect;
            DisconnectEvent += TerminalPort.Disconnect;
            OnConnectToPort();
        }
        
        public void DisconnectFromPort()
        {
            OnDisconnectFromPort();
            ConnectEvent -= TerminalPort.Connect;
            DisconnectEvent -= TerminalPort.Disconnect;
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
            CallEvent?.Invoke(this, new CallEventArgs(TerminalPort.Number, targetNumberTerminal));
        }
        
        protected virtual void OnAnswerCall()
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(TerminalPort.Number));
        }

        protected virtual void OnDropCall()
        {
            DropEvent?.Invoke(this, new DropEventArgs(TerminalPort.Number));
        }
    }
}