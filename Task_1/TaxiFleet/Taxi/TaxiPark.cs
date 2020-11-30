using System.Collections.Generic;
using System.Linq;

namespace TaxiFleet.Taxi
{
    internal class TaxiPark
    {
        private ICollection<Car> _cars;

        public TaxiPark()
        {
            _cars = new List<Car>();
        }
        public TaxiPark(ICollection<Car> cars)
        {
            _cars = new List<Car>(cars);
        }

        public TaxiPark(Car car)
        {
            _cars = new List<Car>()
            {
                car
            };
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
            int totalSum = 0;
        }
        
    }
}