namespace Test.EventArgs
{
    public class CallEventArgs : System.EventArgs
    {
        public string YourNumberTerminal { get; }
        public string TargetNumberTerminal { get; }
        
        public CallEventArgs(string yourNumberTerminal, string targetNumberTerminal)
        {
            YourNumberTerminal = yourNumberTerminal;
            TargetNumberTerminal = targetNumberTerminal;
        }
    }
}