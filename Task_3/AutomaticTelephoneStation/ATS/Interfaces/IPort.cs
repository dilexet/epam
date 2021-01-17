using AutomaticTelephoneStation.ATS.Enums;

namespace AutomaticTelephoneStation.ATS.Interfaces
{
    public interface IPort
    {
        string Number { get; }
        PortState State { get; }
        void Connect();
        void Disconnect();
        void Call();
        void EndCall();
    }
}