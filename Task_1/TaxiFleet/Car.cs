using System;
using TaxiFleet.Enums;
namespace TaxiFleet
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
        
        // public readonly BmwModels BmwModel;
        // public readonly AudiModels AudiModel;
        // public CarClasses Class { get; }
        public Car(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber, 
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
        public override string ToString()
        {
            return $"Brand: {Brand}\n" +
                   $"Model: {Model}\n" +
                   $"Body type: {BodyType}\n" +
                   $"Registration number: {CarRegistrationNumber}\n" +
                   $"Color: {CarColor}\n" +
                   $"Price of car: {PriceOfCar}\n" +
                   $"Fuel consumption: {FuelConsumption}\n" +
                   $"Max speed: {MaxSpeed}\n" +
                   $"Year of creation: {YearOfCreation}\n";
        }

        public virtual void PrintInfo()
        {
            Console.Write(ToString());
        }
    }
}