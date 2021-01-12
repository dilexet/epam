namespace ATS.EventArgs
{
    public class AnswerEventArgs : System.EventArgs
    {
        public string CallerNumberTerminal { get; }
        public string TargetNumberTerminal { get; }
        // public PortState State { get; set; }
        
        public AnswerEventArgs(string callerNumberTerminal, string targetNumberTerminal)
        {
            CallerNumberTerminal = callerNumberTerminal;
            TargetNumberTerminal = targetNumberTerminal;
            // State = state;
        }
    }
}