using System.Collections.Generic;
using System.Linq;

namespace TaxiFleet.Library.Models
{
    public class TaxiStation
    {
        private ICollection<CarBase> _cars;
        public TaxiStation(IEnumerable<CarBase> cars)
        {
            _cars = new List<CarBase>(cars);
        }
        
        public IEnumerable<CarBase> GetCars()
        {
            return _cars;
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
    }
}