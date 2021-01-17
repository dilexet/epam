namespace AutomaticTelephoneStation.ATS
{
    public class TerminalBuilder
    {
        private readonly string _number;

        public TerminalBuilder(string number)
        {
            _number = number;
        }

        public Terminal GetNewTerminal()
        {
            Port port = new Port(_number);
            Terminal terminal = new Terminal(port);
            return terminal;
        }
        
    }
}