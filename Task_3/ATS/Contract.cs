using System;
using ATS.Enums;

namespace ATS
{
    public class Contract
    {
        public Client Client { get; }
        public TariffType TariffType { get; }
        public DateTime ContractStartDate { get; }
        public Terminal Terminal { get; set; }

        public Contract(Client client, TariffType tariffType)
        {
            Client = client;
            TariffType = tariffType;
            ContractStartDate = DateTime.Now;
        }
    }
}