using System;
using System.Collections.Generic;
using TaxiFleet.Enum;
using TaxiFleet.Enum.Models;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            ICollection<Car> cars = new List<Car>()
            {
                new PassengerTaxi(CarBrands.Bmw, BmwModels.I8 ,1500000, 4),
                new PassengerTaxi(CarBrands.Audi, AudiModels.A7 ,210000, 4),
                new PassengerTaxi(CarBrands.Bmw, BmwModels.X6 ,180000, 4)
            };
            TaxiPark taxiPark = new TaxiPark(cars);
            Console.WriteLine(taxiPark.ToString());
        }
    }
}