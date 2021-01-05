using System;


namespace Test
{
    public class Contract
    {
        public Guid Id { get; set; }
        public DateTime ContractStartDate { get; set; }
        public Nullable<DateTime> ContractCloseDate { get; set; }
        public Client Client { get; set; }
        public TariffPlan TariffPlan { get; set; }
        public Terminal Terminal { get; set; }

        public Contract()
        {
            
        }
    }
}