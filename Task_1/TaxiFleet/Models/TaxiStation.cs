
using System;
using System.Collections.Generic;
using System.Linq;
using TaxiFleet.Mocks;

namespace TaxiFleet.Models
{
    public class TaxiStation
    {
        private ICollection<CarBase> _cars;
        public TaxiStation()
        {
            MockCars mockCars = new MockCars();
            _cars = new List<CarBase>(mockCars.GetCars);
        }
        
        public IEnumerable<CarBase> GetCars()
        {
            return _cars;
        }
        
        public void Add(CarBase car)
        {
            _cars.Add(car);
        }

        public void Clear()
        {
            _cars.Clear();
        }

        public void Remove(CarBase car)
        {
            _cars.Remove(car);
        }
        
        public IEnumerable<CarBase> RemoveOldCars(ushort minYear)
        {
            var sortedList = from item in _cars
                where item.YearOfCreation >= minYear
                select item;
            return sortedList;
        }
        public IEnumerable<CarBase> SortingByFuelConsumption()
        {
            var sortingByConsumption = from item in _cars
                orderby item.FuelConsumption
                select item;
            return sortingByConsumption;
        }

        public IEnumerable<CarBase> SelectSpeedTaxi(int minSpeed, int maxSpeed)
        {
            var selectSpeedTaxi = from car in _cars
                where car.MaxSpeed >= minSpeed && car.MaxSpeed <= maxSpeed
                select car;
            return selectSpeedTaxi;
        }
        public double TaxiFleetCost()
        {
            double costAllCars = 0;
            foreach (var car in _cars)
                costAllCars += car.PriceOfCar;
            
            return costAllCars;
        }
        public void Print()
        {
            Info.PrintInfo(_cars);
        }
    }
}