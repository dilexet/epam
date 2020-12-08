using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TaxiFleet.Enums;
using TaxiFleet.Mocks;
using TaxiFleet.Models;

namespace TaxiFleet.UnitTests
{
    [TestFixture]
    public class TaxiFleetTests
    {
        [Test]
        public void TaxiFleetCostTests()
        {
            // arrange

            double expected = 94000;

            // act

            TaxiStation taxiStation = new TaxiStation();
            double actual = taxiStation.TaxiFleetCost();

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SelectSpeedTaxiTests()
        {
            
            // arrange
            
            MockCategoryTaxi mockCategoryTaxi = new MockCategoryTaxi();
            var expected = new List<CarBase>
            {
                new PassengerTaxi(CarBrand.Bmw, "X6", CarBody.Crossover,
                    "8734-KC", CarColor.Black, 24000, 1.8f, 220, 2015,
                    mockCategoryTaxi.GetCategory(TaxiClass.Comfort), 4, 3.92f),
                new PassengerTaxi(CarBrand.Audi, "A3", CarBody.Hatchback,
                    "8361-PB", CarColor.Blue, 30000, 3.6f, 210, 2016,
                    mockCategoryTaxi.GetCategory(TaxiClass.Business), 4, 8.17f)
            };
            
            // act

            TaxiStation taxiStation = new TaxiStation();
            var actual = taxiStation.SelectSpeedTaxi(210, 220).ToList();

            // assert

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SortingByFuelConsumptionTests()
        {
            
            // arrange
            
            MockCategoryTaxi mockCategoryTaxi = new MockCategoryTaxi();
            var expected = new List<CarBase>
            {
                new PassengerTaxi(CarBrand.Bmw, "X6", CarBody.Crossover,
                    "8734-KC", CarColor.Black, 24000, 1.8f, 220, 2015, 
                    mockCategoryTaxi.GetCategory(TaxiClass.Comfort), 4, 3.92f),
               
                new PassengerTaxi(CarBrand.Volkswagen, "Caddy 5", CarBody.Minivan,
                    "6523-LV", CarColor.Green, 15000, 2.4f, 200, 2005, 
                    mockCategoryTaxi.GetCategory(TaxiClass.Compactvan), 6, 5.79f),
              
                new PassengerTaxi(CarBrand.Audi, "A3", CarBody.Hatchback, 
                    "8361-PB", CarColor.Blue, 30000, 3.6f, 210, 2016, 
                    mockCategoryTaxi.GetCategory(TaxiClass.Business), 4, 8.17f),
              
                new CargoTaxi(CarBrand.Citroen,"Jumpy 3", CarBody.Minivan, 
                    "9274-MP", CarColor.White, 14000, 5.1f, 140, 2016, 
                    mockCategoryTaxi.GetCategory(TaxiClass.Cargo), 10f, 8.5f),
              
                new CargoTaxi(CarBrand.Peugeot, "Boxer Van 3", CarBody.Van,
                    "7582-DN", CarColor.Red, 11000, 5.3f, 150, 2000, 
                    mockCategoryTaxi.GetCategory(TaxiClass.Cargo), 15f, 12.5f),
            };
            
            // act

            TaxiStation taxiStation = new TaxiStation();
            var actual = taxiStation.SortingByFuelConsumption().ToList();

            // assert

            Assert.AreEqual(expected, actual);
        }
    }
}

 