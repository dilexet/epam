namespace Test.EventArgs
{
    public class DropEventArgs : System.EventArgs, ICallEventsArgs
    {
        public string YourNumberTerminal { get; }
        
        public DropEventArgs(string yourNumberTerminal)
        {
            YourNumberTerminal = yourNumberTerminal;
        }
    }
}