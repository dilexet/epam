using AutomaticTelephoneStation.BillingSystem.Enums;

namespace AutomaticTelephoneStation.BillingSystem
{
    public class Tariff
    {
        public double CostPerMinute { get; }

        public Tariff(TariffType type)
        {
            switch (type)
            {
                case TariffType.Light:
                {
                    CostPerMinute = 2.5;
                    break;
                }
                case TariffType.Standart :
                {
                    CostPerMinute = 3.5;
                    break;
                }
                case TariffType.Pro :
                {
                    CostPerMinute = 4.5;
                    break;
                }
                default :
                {
                    CostPerMinute = 0;
                    break;
                }
            }
        }
    }
}