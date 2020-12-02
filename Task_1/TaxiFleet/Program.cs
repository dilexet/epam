using System;
using System.Collections.Generic;
using TaxiFleet.Data.Mocks;
using TaxiFleet.Enums;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            MockCars mockCars = new MockCars();
            TaxiPark taxiPark = new TaxiPark(mockCars.GetCars);
            //taxiPark.PrintInfo();
            // taxiPark.TotalRevenueOfTaxiPark();
            Customer person1 = new Customer("Maksim");
            person1.TaxiOrdering(taxiPark);
            taxiPark.SortingByFuelConsumption();
            var car = taxiPark.SelectSpeedTaxi();
            
            
            foreach (var item in car)
            {
                item.PrintInfo();
            }
            
        }
    }
}