using System;

namespace Test
{
    public class Terminal
    {
        private Guid _id;
        private string _terminalNumber;

        public Terminal(string terminalNumber)
        {
            _id = Guid.NewGuid();
            _terminalNumber = terminalNumber;
        }

        private void Call()
        {
            
        }

        private void Drop()
        {
            
        }

        private void Answer()
        {
            
        }
    }
}