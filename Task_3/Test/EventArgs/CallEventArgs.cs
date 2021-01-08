namespace Test.EventArgs
{
    public class CallEventArgs : System.EventArgs
    {
        public string CallerNumberTerminal { get; }
        public string TargetNumberTerminal { get; }
        
        public CallEventArgs(string callerNumberTerminal, string targetNumberTerminal)
        {
            CallerNumberTerminal = callerNumberTerminal;
            TargetNumberTerminal = targetNumberTerminal;
        }
    }
}