namespace AutomaticTelephoneStation.EventArgs
{
    public class AnswerEventArgs : System.EventArgs
    {
        public string TargetNumberTerminal { get; }

        public AnswerEventArgs(string targetNumberTerminal)
        {
            TargetNumberTerminal = targetNumberTerminal;
        }
    }
}