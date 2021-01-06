namespace Test.EventArgs
{
    public class AnswerEventArgs: System.EventArgs
    {
        private readonly string _yourNumberTerminal;
        private readonly string _targetNumberTerminal;
        // TODO: Возможно понадобиться Enum [состояние вызова] или PortState
        
        public AnswerEventArgs(string yourNumberTerminal, string targetNumberTerminal)
        {
            _yourNumberTerminal = yourNumberTerminal;
            _targetNumberTerminal = targetNumberTerminal;
        }
    }
}