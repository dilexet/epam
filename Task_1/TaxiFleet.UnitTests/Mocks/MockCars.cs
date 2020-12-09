using System.Collections.Generic;
using TaxiFleet.Enums;
using TaxiFleet.Models;

namespace TaxiFleet.Mocks
{
    public class MockCars
    {
        private readonly MockCategoryTaxi _mockCategoryTaxi = new MockCategoryTaxi();

        public IEnumerable<CarBase> GetCars
        {
            get
            {
                return new List<CarBase>
                {
                    new CargoTaxi(CarBrand.Peugeot, "Boxer Van 3", CarBody.Van,
                        "7582-DN", CarColor.Red, 11000, 5.3f, 150, 2000, 
                        _mockCategoryTaxi.GetCategory(TaxiClass.Cargo), 15f, 12.5f),
                
                    new PassengerTaxi(CarBrand.Bmw, "X6", CarBody.Crossover,
                        "8734-KC", CarColor.Black, 24000, 1.8f, 220, 2015, 
                        _mockCategoryTaxi.GetCategory(TaxiClass.Comfort), 4, 3.92f),
                
                    new PassengerTaxi(CarBrand.Volkswagen, "Caddy 5", CarBody.Minivan,
                        "6523-LV", CarColor.Green, 15000, 2.4f, 200, 2005, 
                        _mockCategoryTaxi.GetCategory(TaxiClass.Compactvan), 6, 5.79f),
                    
                    new CargoTaxi(CarBrand.Citroen,"Jumpy 3", CarBody.Minivan, 
                        "9274-MP", CarColor.White, 14000, 5.1f, 140, 2016, 
                        _mockCategoryTaxi.GetCategory(TaxiClass.Cargo), 10f, 8.5f),
                    
                    new PassengerTaxi(CarBrand.Audi, "A3", CarBody.Hatchback, 
                        "8361-PB", CarColor.Blue, 30000, 3.6f, 210, 2016, 
                        _mockCategoryTaxi.GetCategory(TaxiClass.Business), 4, 8.17f),
                    
                };
            }
        }
    }
}