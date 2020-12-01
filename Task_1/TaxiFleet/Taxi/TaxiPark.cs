using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaxiFleet.Taxi
{
    internal class TaxiPark
    {
        private List<Car> _cars;
        
        public TaxiPark(ICollection<Car> cars)
        {
            _cars = new List<Car>(cars);
        }

        public IEnumerator GetEnumerator()
        {
            return _cars.GetEnumerator();
        }
        
        public int Count
        {
            get
            {
                return _cars.Count;
            }
        }

        public Car this[int index]
        {
            get
            {
                return _cars[index];
            }
            set
            {
                _cars[index] = value;
            }
        }

        public void PrintInfo()
        {
            foreach (var car in _cars)
            {
                car.PrintInfo();
            }
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Clear()
        {
            _cars.Clear();
        }

        public void Remove(Car car)
        {
            _cars.Remove(car);
        }

        public void RemoveAt(int index)
        {
            _cars.RemoveAt(index);
        }
        public void RemoveOldCars(ushort minYear)
        {
            var sortedList = from item in _cars
                where item.YearOfCreation >= minYear
                select item;
            _cars = sortedList.ToList();
        }
        public void SortingByFuelConsumption()
        {
            var sortingByConsumption = from item in _cars
                orderby item.FuelConsumption
                select item;
            _cars = sortingByConsumption.ToList();
        }
        public void TotalRevenueOfTaxiPark()
        {
            double costAllCars = 0;
            float rentalIncome = 0f;
            foreach (var car in _cars)
            {
                costAllCars += car.PriceOfCar;
                CargoTaxi cargoTaxi = car as CargoTaxi;
                if (cargoTaxi != null)
                {
                    rentalIncome += cargoTaxi.RentPerHour;
                    continue;
                }
                PassengerTaxi passengerTaxi = car as PassengerTaxi;
                if (passengerTaxi != null)
                    rentalIncome += passengerTaxi.RentPerHour;

            }

            Console.WriteLine($"The cost of all cars of the taxi fleet: {costAllCars}$\n" +
                              $"Taxi fleet rental income: {rentalIncome}$ per hour\n\n");
        }
        
    }
}