using TaxiFleet.Enums;

namespace TaxiFleet.Data.Models
{
    internal abstract class Car
    {
        public readonly CarBrands Brand;
        public readonly string Model;
        public readonly BodyTypes BodyType;
        public readonly string CarRegistrationNumber;
        public readonly CarColors CarColor;
        public readonly double PriceOfCar; // Цена самого автомобиля
        public readonly float FuelConsumption; // расход топлива на 100 км
        public readonly ushort MaxSpeed;
        public readonly ushort YearOfCreation;
        
        protected Car(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber, 
            CarColors carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation)
        {
            Brand = brand;
            Model = model;
            BodyType = bodyType;
            CarRegistrationNumber = carRegistrationNumber;
            CarColor = carColor;
            PriceOfCar = priceOfCar;
            FuelConsumption = fuelConsumption;
            MaxSpeed = maxSpeed;
            YearOfCreation = yearOfCreation;
        }
    }
}