using System.Linq;
using NUnit.Framework;
using TaxiFleet.Library.Models;
using TaxiFleet.UnitTests.Mocks;

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
            TaxiStation taxiStation = new TaxiStation(mockCars.GetCars);
            FakeMockCars fakeMockCars = new FakeMockCars();
            var expected = fakeMockCars.GetCars.ToList();

            // act
            var actual = taxiStation.SortingByFuelConsumption().ToList();

            // assert
            Assert.That(expected, Is.EqualTo(actual).Using(new EqualityComparer()));
        }
    }
}
 