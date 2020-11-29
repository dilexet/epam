
using System;
using System.Collections.Generic;

namespace TaxiFleet
{
    internal enum CarClasses
    {
        A,
        B,
        C,
        D,
        E,
        F,
        J,
        M,
        S
    }
    public enum CarBrands
    {
        Bugatti,
        Tesla,
        RollsRoyce,
        Lamborghini,
        Bmw
    }

    public enum TransmissionTypes
    {
        Mechanical,
        Automatic,
        Robotic,
        Variable
    }

    public enum BodyTypes
    {
        Sedan,
        Hatchback,
        Pickup,
        Roadster,
        Convertible,
        Coupe,
        Minivan,
        Suv,
        StationWagon
    }

    interface IBody
    {
        BodyTypes BodyType { get; }
        byte NumberOfDoors { get; }
        byte NumberOfSeats { get; }
        float TrunkVolume { get; }
    }
    interface IEngine
    {
        TransmissionTypes TransmissionType { get; }
        float Volume { get; }
        ushort Power { get; }
        double FuelConsumption { get; }
    }
    internal class Car: IEngine, IBody
    {
        // IEngine
        
        // Вопрос: Почему нельзя написать private?
        public float Volume { get; }
        public ushort Power { get; }
        public TransmissionTypes TransmissionType { get; }
        public double FuelConsumption { get; }
        
        // IBody
        public BodyTypes BodyType { get; }
        public byte NumberOfDoors { get; }
        public byte NumberOfSeats { get; }
        public float TrunkVolume { get; }
        
        // Car
        public CarBrands Brand { get; }
        public CarClasses Class { get; }
        public ushort MaxSpeed { get; }
        public ushort YearOfCreation { get; }
        public double Price { get; }
        public float Rental { get; }
        public double Leasing { get; }
        
        public Car(CarBrands brand, CarClasses @class, ushort maxSpeed, ushort yearOfCreation, double price, float rental, double leasing)
        {
            Brand = brand;
            Class = @class;
            MaxSpeed = maxSpeed;
            YearOfCreation = yearOfCreation;
            Price = price;
            Rental = rental;
            Leasing = leasing;
        }

        public override string ToString()
        {
            return $"";
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Car(CarBrands.Bmw,CarClasses.C, 350, 2000, 300000, 2000, 20000));
        }
    }
}