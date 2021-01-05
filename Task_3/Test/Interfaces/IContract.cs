using System;
using Test.Enums;

namespace Test.Interfaces
{
    public interface IContract
    {
        Client Client { get; set; }
        TariffType TariffType { get; set; }
        DateTime ContractStartDate { get; set; }
        Nullable<DateTime> ContractCloseDate { get; set; }
    }
}