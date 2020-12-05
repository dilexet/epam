using TaxiFleet.Enums;

namespace TaxiFleet.Data.Models
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