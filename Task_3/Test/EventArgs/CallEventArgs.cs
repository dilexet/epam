namespace Test.EventArgs
{
    public class CallEventArgs : System.EventArgs
    {
        private readonly string _yourNumberTerminal;
        private readonly string _targetNumberTerminal;

        public CallEventArgs(string yourNumberTerminal, string targetNumberTerminal)
        {
            _yourNumberTerminal = yourNumberTerminal;
            _targetNumberTerminal = targetNumberTerminal;
        }
    }
}