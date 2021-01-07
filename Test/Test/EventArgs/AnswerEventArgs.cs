using Test.Enums;

namespace Test.EventArgs
{
    public class AnswerEventArgs : System.EventArgs, ICallEventsArgs
    {
        public string YourNumberTerminal { get; }
        public string TargetNumberTerminal { get; }
        public PortState State { get; set; }
        
        public AnswerEventArgs(string yourNumberTerminal, string targetNumberTerminal)
        {
            YourNumberTerminal = yourNumberTerminal;
            TargetNumberTerminal = targetNumberTerminal;
        }
    }
}