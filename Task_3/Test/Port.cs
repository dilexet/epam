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
    }
}