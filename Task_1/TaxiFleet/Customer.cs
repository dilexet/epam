using System;
using System.Threading;
using TaxiFleet.Enums;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class Customer
    {
        private string _name;
        private string _routeStartAddress;
        private string _routeEndAddress;
        public Customer(string name)
        {
            _name = name;
        }

        public void TaxiOrdering(TaxiPark taxiPark)
        {
            Random random = new Random();
            RouteSelection();
            TaxiClasses taxiClass = TaxiClassSelection();
            if (taxiClass == TaxiClasses.Cargo)
            {
                Console.Write("Cargo volume: ");
                float.TryParse(Console.ReadLine(), out float volume);
                
                CargoTaxi taxi = SearchCargoTaxi(taxiPark);
                WaitingTaxi(taxi);
                
                Console.WriteLine(
                    $"{_name} is waiting for you {taxi.Brand} {taxi.Model}\n" +
                    $"{_routeStartAddress} - {_routeEndAddress}\n" +
                    $"To pay {taxi.CostOfTrip * volume}$\n");
            }
            else
            {
                PassengerTaxi taxi = SearchPassengerTaxi(taxiPark);
                WaitingTaxi(taxi);
                
                Console.WriteLine(
                    $"{_name} is waiting for you {taxi.Brand} {taxi.Model}\n" +
                    $"{_routeStartAddress} - {_routeEndAddress}\n" +
                    $"To pay {taxi.CostOfTrip * random.Next(1, 40)}$\n");
            }
        }
        private void RouteSelection()
        {
            Console.Write("Enter your address: ");
            _routeStartAddress = Console.ReadLine();
            Console.Write("Where we go: ");
            _routeEndAddress = Console.ReadLine();
        }

        private TaxiClasses TaxiClassSelection()
        {
            Console.WriteLine($"{(int)TaxiClasses.Economy}. {TaxiClasses.Economy}\n" +
                              $"{(int)TaxiClasses.Comfort}. {TaxiClasses.Comfort}\n" +
                              $"{(int)TaxiClasses.Business}. {TaxiClasses.Business}\n" +
                              $"{(int)TaxiClasses.Minivan}. {TaxiClasses.Minivan}\n" +
                              $"{(int)TaxiClasses.Compactvan}. {TaxiClasses.Compactvan}\n" +
                              $"{(int)TaxiClasses.Cargo}. {TaxiClasses.Cargo}\n");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    return TaxiClasses.Comfort;
                case ConsoleKey.D2:
                    return TaxiClasses.Business;
                case ConsoleKey.D3:
                    return TaxiClasses.Minivan;
                case ConsoleKey.D4:
                    return TaxiClasses.Compactvan;
                case ConsoleKey.D5:
                    return TaxiClasses.Cargo;
                default:
                    return TaxiClasses.Economy;
            }
        }

        private CargoTaxi SearchCargoTaxi(TaxiPark taxiPark)
        {
            Random random = new Random();
            return taxiPark.CargoTaxis[random.Next(taxiPark.CargoTaxis.Count - 1)];
        }
        private PassengerTaxi SearchPassengerTaxi(TaxiPark taxiPark)
        {
            Random random = new Random();
            return taxiPark.PassengerTaxis[random.Next(taxiPark.PassengerTaxis.Count - 1)];
        }

        private void WaitingTaxi(Car taxi)
        {
            Random random = new Random();
            Console.WriteLine(
                $"In {random.Next(1, 15)} minute {taxi.Brand} {taxi.Model} will arrive, color {taxi.CarColor}, number {taxi.CarRegistrationNumber}");
            Thread.Sleep(5000);
            Console.Clear();
        }
    }
}