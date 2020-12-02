using System.Collections;
using System.Collections.Generic;
using TaxiFleet.Enums;

namespace TaxiFleet.Data.Mocks
{
    internal class MockCategoryTaxi
    {
        private IEnumerable AllCategoriesTaxi
        {
            get
            {
                return new List<CategoryTaxi>
                {
                    new CategoryTaxi(TaxiClasses.Economy, "Balance of price and quality"),
                    new CategoryTaxi(TaxiClasses.Comfort, "High level of convenience"),
                    new CategoryTaxi(TaxiClasses.Compactvan, "Small company or large luggage"),
                    new CategoryTaxi(TaxiClasses.Business, "Spacious interior and attention to detail"),
                    new CategoryTaxi(TaxiClasses.Minivan, "Plenty of space for a friendly company and luggage"),
                    new CategoryTaxi(TaxiClasses.Cargo, "Fast delivery of any cargo")
                };
            }
        }

        public CategoryTaxi GetCategory(TaxiClasses taxiClass)
        {
            foreach (var category in AllCategoriesTaxi)
            {
                CategoryTaxi categoryTaxi = category as CategoryTaxi;
                if (categoryTaxi != null && categoryTaxi.TaxiClass == taxiClass) 
                    return categoryTaxi;
            }
            return null;
        }
    }
}