namespace Test.EventArgs
{
    public class DropEventArgs : System.EventArgs
    {
        public string YourNumberTerminal { get; }
        
        public DropEventArgs(string yourNumberTerminal)
        {
            YourNumberTerminal = yourNumberTerminal;
        }
    }
}