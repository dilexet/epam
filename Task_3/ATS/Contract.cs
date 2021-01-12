using System;
using ATS.Enums;

namespace ATS
{
    public class Contract
    {
        public Client Client { get; }
        public TariffType TariffType { get; }
        public DateTime ContractStartDate { get; }
        public DateTime? ContractCloseDate { get; }

        public Contract(Client client, TariffType tariffType)
        {
            Client = client;
            TariffType = tariffType;
            ContractStartDate = DateTime.Now;
            ContractCloseDate = null;
        }
    }
}