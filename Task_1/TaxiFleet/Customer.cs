using System;
using TaxiFleet.Taxi;

namespace TaxiFleet
{
    // сделать: при заказе выбор класса такси, вывод суммы к оплате, исходя из растояния(генерить рандомно)
    internal class Customer
    {
        private string _name;
        private string RouteStartAddress;
        private string RouteEndAddress;
        public Customer(string name)
        {
            _name = name;
        }

        public void TaxiOrdering(TaxiPark taxiPark)
        {
            Console.Write("Enter your address: ");
            RouteStartAddress = Console.ReadLine();
            Console.Write("Where we go: ");
            RouteEndAddress = Console.ReadLine();
            Random random = new Random();
            PassengerTaxi taxi;
            do
            {
                taxi = taxiPark[random.Next(taxiPark.Count - 1)] as PassengerTaxi;
            } while (taxi == null);

            Console.WriteLine(
                $"In {random.Next(1, 15)} minute {taxi.Brand} {taxi.Model} will arrive, color {taxi.CarColor}, number {taxi.CarRegistrationNumber}");
        }
    }
}