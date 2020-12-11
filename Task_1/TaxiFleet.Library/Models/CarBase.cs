using TaxiFleet.Library.Enums;

namespace TaxiFleet.Library.Models
{
    public abstract class CarBase
    {
        public CarBrand Brand { get; }
        public string Model { get; }
        public CarBody CarBody { get; }
        public string CarRegistrationNumber { get; }
        public CarColor CarColor { get; }
        public double PriceOfCar { get; }
        public float FuelConsumption { get; }
        public ushort MaxSpeed { get; }
        public ushort YearOfCreation { get; }
        
        protected CarBase(CarBrand brand, string model, CarBody carBody, string carRegistrationNumber, 
            CarColor carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation)
        {
            Brand = brand;
            Model = model;
            CarBody = carBody;
            CarRegistrationNumber = carRegistrationNumber;
            CarColor = carColor;
            PriceOfCar = priceOfCar;
            FuelConsumption = fuelConsumption;
            MaxSpeed = maxSpeed;
            YearOfCreation = yearOfCreation;
        }
        
    }
}