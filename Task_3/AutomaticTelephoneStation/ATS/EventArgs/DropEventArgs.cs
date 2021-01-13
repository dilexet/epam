namespace AutomaticTelephoneStation.ATS.EventArgs
{
    public class DropEventArgs : System.EventArgs
    {
        public string CallerNumberTerminal { get; }
        
        public DropEventArgs(string callerNumberTerminal)
        {
            CallerNumberTerminal = callerNumberTerminal;
        }
    }
}