using TaxiFleet.Enums;

namespace TaxiFleet.Models
{
    public class CategoryTaxi
    {
        public TaxiClass TaxiClass { get; }
        public string Description { get; }

        public CategoryTaxi(TaxiClass taxiClass, string description)
        {
            TaxiClass = taxiClass;
            Description = description;
        }
    }
}