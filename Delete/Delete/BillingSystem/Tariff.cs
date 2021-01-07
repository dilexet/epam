using Delete.Enums;

namespace Delete.BillingSystem
{
    public class Tariff
    {
        public int CostOfMonth { get; private set; }
        public int CostOfCallPerMinute { get; private set; }
        public int LimitCallInMonth { get; private set; }
        public TariffType TariffType { get; private set; }
        public Tariff(TariffType type)
        {
            TariffType = type;
            switch (TariffType)
            {
                case TariffType.Light:
                    {
                        CostOfMonth = 10;
                        LimitCallInMonth = 4;
                        CostOfCallPerMinute = 3;
                        break;
                    }
                case TariffType.Standart :
                    {
                        CostOfMonth = 20;
                        LimitCallInMonth = 8;
                        CostOfCallPerMinute = 2;
                        break;
                    }
                case TariffType.Pro :
                    {
                        CostOfMonth = 30;
                        LimitCallInMonth = 12;
                        CostOfCallPerMinute = 1;
                        break;
                    }
                default :
                    {
                        CostOfMonth = 0;
                        LimitCallInMonth = 0;
                        CostOfCallPerMinute = 0;
                        break;
                    }
            }
        }
    }
}
