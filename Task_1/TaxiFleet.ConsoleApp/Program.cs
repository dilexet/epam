using System;
using TaxiFleet.Library.Mocks;
using TaxiFleet.Library.Models;

namespace TaxiFleet.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            MockCars mockCars = new MockCars();
            TaxiStation taxiStation = new TaxiStation(mockCars.GetCars);
            
            Console.WriteLine("\n_______List of cars in the taxi company_______\n");
            foreach (var car in taxiStation.GetCars())
            {
                Console.WriteLine(car.ToString());
            }
            
            Console.WriteLine("\n_______Taxi fleet cost_______\n");
            Console.WriteLine($"Cost is {taxiStation.TaxiFleetCost()} $");
            
            Console.WriteLine("\n_______Sorted cars of the taxi fleet by fuel consumption_______\n");
            foreach (var car in taxiStation.SortingByFuelConsumption())
            {
                Console.WriteLine(car.ToString());
            }
            
            Console.WriteLine("\n_______List of cars in the taxi company corresponding to the specified speed range_______\n");
            foreach (var car in taxiStation.SelectSpeedTaxi(210, 220)) 
            {
                Console.WriteLine(car.ToString());
            }
            
            
        }
    }
}