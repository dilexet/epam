using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaxiFleet.Data.Mocks;
using TaxiFleet.Data.Models;

namespace TaxiFleet.Taxi
{
    // TODO: Разбить класс, вынести некоторую функциональность в отдельный класс
    internal class TaxiPark
    {
        private List<Car> _cars;
        public readonly List<PassengerTaxi> PassengerTaxis = new List<PassengerTaxi>();
        public readonly List<CargoTaxi> CargoTaxis = new List<CargoTaxi>();

        public TaxiPark()
        {
            MockCars mockCars = new MockCars();
            _cars = new List<Car>(mockCars.GetCars);
        }
        public TaxiPark(IEnumerable<Car> cars)
        {
            _cars = new List<Car>(cars);
            foreach (var car in _cars)
            {
                if (car as PassengerTaxi != null)
                    PassengerTaxis.Add(car as PassengerTaxi);
                else
                    CargoTaxis.Add(car as CargoTaxi);
            }
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

        public IEnumerable<Car> SelectSpeedTaxi(int minSpeed = 230, int maxSpeed = 250)
        {
            var selectSpeedTaxi = from car in _cars
                where car.MaxSpeed >= minSpeed && car.MaxSpeed <= maxSpeed
                select car;
            return selectSpeedTaxi;
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

        public void Print()
        {
            Info.PrintInfo(_cars);
        }
    }
}