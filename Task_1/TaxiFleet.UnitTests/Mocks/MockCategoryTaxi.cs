using System.Collections;
using System.Collections.Generic;
using TaxiFleet.Library.Enums;
using TaxiFleet.Library.Models;

namespace TaxiFleet.UnitTests.Mocks
{
    public class MockCategoryTaxi
    {
        private IEnumerable AllCategoriesTaxi
        {
            get
            {
                return new List<CategoryTaxi>
                {
                    new CategoryTaxi(TaxiClass.Economy, "Balance of price and quality"),
                    new CategoryTaxi(TaxiClass.Comfort, "High level of convenience"),
                    new CategoryTaxi(TaxiClass.Compactvan, "Small company or large luggage"),
                    new CategoryTaxi(TaxiClass.Business, "Spacious interior and attention to detail"),
                    new CategoryTaxi(TaxiClass.Minivan, "Plenty of space for a friendly company and luggage"),
                    new CategoryTaxi(TaxiClass.Cargo, "Fast delivery of any cargo")
                };
            }
        }

        public CategoryTaxi GetCategory(TaxiClass taxiClass)
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