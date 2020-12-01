using System;
using System.Threading;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    // сделать: при заказе выбор класса такси, вывод суммы к оплате, исходя из растояния(генерить рандомно)
    // сделать: разбитьь функцию TaxiOrdering на несколько маленьких
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
            Console.Write("Enter your address: ");
            _routeStartAddress = Console.ReadLine();
            Console.Write("Where we go: ");
            _routeEndAddress = Console.ReadLine();
            
            Random random = new Random();
            PassengerTaxi taxi;
            do
            {
                taxi = taxiPark[random.Next(taxiPark.Count - 1)] as PassengerTaxi;
            } while (taxi == null);

            Console.WriteLine(
                $"In {random.Next(1, 15)} minute {taxi.Brand} {taxi.Model} will arrive, color {taxi.CarColor}, number {taxi.CarRegistrationNumber}");
            Thread.Sleep(5000);
            Console.Clear();
            Console.WriteLine(
                $"{_name} is waiting for you {taxi.Brand} {taxi.Model}\n" +
                $"{_routeStartAddress} - {_routeEndAddress}\n" +
                $"To pay {taxi.CostOfTrip * random.Next(1, 40)}$\n");

        }
    }
}