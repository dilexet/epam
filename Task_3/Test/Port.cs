using Test.Enums;

namespace Test
{
    public class Port
    {
        private PortState _portState = PortState.Off;
        
        public PortState PortState
        {
            get { return _portState; }
            set
            {
                // TODO: добавить проверку
                _portState = value;
            }
        }
        // TODO: добавить события которые обрабатывали изменения состояния порта

        public void Connect()
        {
            if (_portState == PortState.Off)
            {
                _portState = PortState.On;
            }
        }
        public void Disconnect()
        {
            if (_portState == PortState.On)
            {
                _portState = PortState.Off;
            }
        }
    }
}