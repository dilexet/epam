using TaxiFleet.Library.Enums;

namespace TaxiFleet.Library.Models
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