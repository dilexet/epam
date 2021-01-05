using System;
using Test.Enums;
using Test.Interfaces;

namespace Test
{
    public class Contract: IContract
    {
        public Client Client { get; set; }
        public TariffType TariffType { get; set; }
        public DateTime ContractStartDate { get; set; }
        public Nullable<DateTime> ContractCloseDate { get; set; }

        public Contract(Client client, TariffType tariffType)
        {
            Client = client;
            TariffType = tariffType;
            ContractStartDate = DateTime.Now;
            ContractCloseDate = null;
        }
    }
}