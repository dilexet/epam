using TaxiFleet.Enums;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class CategoryTaxi
    {
        public readonly TaxiClasses TaxiClass;
        public readonly string Description;

        public CategoryTaxi(TaxiClasses taxiClass, string description)
        {
            TaxiClass = taxiClass;
            Description = description;
        }
    }
}