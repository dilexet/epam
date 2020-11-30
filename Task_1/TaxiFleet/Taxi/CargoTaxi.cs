using TaxiFleet.Enum;
using TaxiFleet.Enum.Models;

namespace TaxiFleet.Taxi
{
    internal class CargoTaxi : Car
    {
        public float _capacity;
        // public float _costForRent;
        
        public CargoTaxi(CarBrands brand, AudiModels model, double priceOfCar,
            float capacity) 
            : base(brand, model, priceOfCar)
        {
            _capacity = capacity;
        }
        
        public CargoTaxi(CarBrands brand, BmwModels model, double priceOfCar, 
            float capacity) 
            : base(brand, model, priceOfCar)
        {
            _capacity = capacity;
        }
    }
}