using TaxiFleet.Enum;
using TaxiFleet.Enum.Models;

namespace TaxiFleet.Taxi
{
    internal class PassengerTaxi: Car
    {
        public byte _numberOfPassenger;
        //private float _costOfTrip;

        public PassengerTaxi(CarBrands brand, AudiModels model, double priceOfCar,
            byte numberOfPassenger) 
            : base(brand, model, priceOfCar)
        {
            _numberOfPassenger = numberOfPassenger;
        }
        
        public PassengerTaxi(CarBrands brand, BmwModels model, double priceOfCar, 
            byte numberOfPassenger) 
            : base(brand, model, priceOfCar)
        {
            _numberOfPassenger = numberOfPassenger;
        }
        
    }
}