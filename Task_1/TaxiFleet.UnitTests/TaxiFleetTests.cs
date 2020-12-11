using System.Linq;
using NUnit.Framework;
using TaxiFleet.Library.Mocks;
using TaxiFleet.Library.Models;

namespace TaxiFleet.UnitTests
{
    [TestFixture]
    public class TaxiFleetTests
    {
        [Test]
        public void TaxiFleetCostTests()
        {
            // arrange
            MockCars mockCars = new MockCars();
            TaxiStation taxiStation = new TaxiStation(mockCars.GetCars);

            double expected = 94000;

            // act

            double actual = taxiStation.TaxiFleetCost();

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SelectSpeedTaxiTests()
        {

            // arrange
            MockCars mockCars = new MockCars();
            TaxiStation taxiStation = new TaxiStation(mockCars.GetCars);

            // act
            short minSpeed = 210;
            short maxSpeed = 220;
            var actual = taxiStation.SelectSpeedTaxi(minSpeed, maxSpeed).ToList();

            // assert
            Assert.IsNotEmpty(actual);
            foreach (var car in actual)
                Assert.That(() => car.MaxSpeed >= minSpeed && car.MaxSpeed <= maxSpeed);
        }
        [Test]
        public void SortingByFuelConsumptionTests()
        {

            // arrange
            MockCars mockCars = new MockCars();
            TaxiStation taxiStation = new TaxiStation(cars: mockCars.GetCars);

            // act
            var actual = taxiStation.SortingByFuelConsumption().ToList();

            // assert
            Assert.IsNotEmpty(actual);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                var index = i;
                Assert.That( () => actual[index].FuelConsumption < actual[index + 1].FuelConsumption);
            }
            
        }
    }
}
 