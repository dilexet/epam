using System;

namespace Delete.Args
{
    public interface ICallingEventArgs
    {
        int TelephoneNumber { get; }
        int TargetTelephoneNumber { get; }
        Guid Id { get; }
    }
}
