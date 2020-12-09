using System;
using System.Collections.Generic;
using TaxiFleet.Library.Models;

namespace TaxiFleet.UnitTests
{
    public class EqualityComparer : IEqualityComparer<CarBase>
    {
        public bool Equals(CarBase car1, CarBase car2)
        {
            return car1 != null &&
                   car2 != null &&
                   car1.Brand == car2.Brand &&
                   car1.Model == car2.Model &&
                   car1.CarBody == car2.CarBody &&
                   car1.CarRegistrationNumber == car2.CarRegistrationNumber &&
                   car1.CarColor == car2.CarColor &&
                   Math.Abs(car1.PriceOfCar - car2.PriceOfCar) < 0.000000001 &&
                   Math.Abs(car1.FuelConsumption - car2.FuelConsumption) < 0.00001 &&
                   car1.MaxSpeed == car2.MaxSpeed &&
                   car1.YearOfCreation == car2.YearOfCreation;
        }

        public int GetHashCode(CarBase obj)
        {
            return obj.GetHashCode();
        }
    }
}