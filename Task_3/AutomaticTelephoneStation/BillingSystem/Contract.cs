using System;
using AutomaticTelephoneStation.ATS;

namespace AutomaticTelephoneStation.BillingSystem
{
    public class Contract
    {
        public Client Client { get; }
        public Tariff Tariff { get; }
        public DateTime ContractStartDate { get; }
        public Terminal Terminal { get; set; }

        public Contract(Client client, Tariff tariff)
        {
            Client = client;
            Tariff = tariff;
            ContractStartDate = DateTime.Now;
        }
    }
}