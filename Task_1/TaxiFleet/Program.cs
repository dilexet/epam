using System;
using System.Collections.Generic;
using TaxiFleet.Enums;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MockCategoryTaxi mockCategoryTaxi = new MockCategoryTaxi();
            ICollection<Car> cars = new List<Car>()
            {
                new CargoTaxi(CarBrands.Peugeot, "Boxer Van 3", BodyTypes.Van,
                    "7582-DN", CarColors.Red, 11000, 2.8f, 210, 2000, mockCategoryTaxi.GetCategory(TaxiClasses.Cargo), 15f),
                
                new PassengerTaxi(CarBrands.Bmw, "X6", BodyTypes.Crossover,
                    "8734-KC", CarColors.Black, 24000, 1.8f, 240, 2015, mockCategoryTaxi.GetCategory(TaxiClasses.Comfort)),
                
                new PassengerTaxi(CarBrands.Volkswagen, "Caddy 5", BodyTypes.Minivan,
                    "6523-LV", CarColors.Green, 15000, 2.4f, 220, 2005, mockCategoryTaxi.GetCategory(TaxiClasses.Compactvan))
            };
            TaxiPark taxiPark = new TaxiPark(cars);
            //taxiPark.PrintInfo();
            // taxiPark.TotalRevenueOfTaxiPark();
            // Customer person1 = new Customer("Maksim");
            // person1.TaxiOrdering(taxiPark);
            taxiPark.SortingByFuelConsumption();
            var car = taxiPark.SelectSpeedTaxi();
            
            // Выводит только характеристика Car
            foreach (var item in car)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}