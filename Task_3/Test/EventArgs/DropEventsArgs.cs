namespace Test.EventArgs
{
    public class DropEventsArgs: System.EventArgs
    {
        private readonly string _yourNumberTerminal;
        private readonly string _targetNumberTerminal;
        // TODO: Возможно понадобиться Enum [состояние вызова] или PortState
        
        public DropEventsArgs(string yourNumberTerminal, string targetNumberTerminal)
        {
            _yourNumberTerminal = yourNumberTerminal;
            _targetNumberTerminal = targetNumberTerminal;
        }
    }
}