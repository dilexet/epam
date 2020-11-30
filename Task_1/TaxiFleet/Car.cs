using TaxiFleet.Enum;
using TaxiFleet.Enum.Models;
using TaxiFleet.Interfaces;

namespace TaxiFleet
{
    internal abstract class Car: IEngine, IBody
    {
        // Вопрос: Почему нельзя написать private?
        
        // IEngine
        public float Volume { get; }
        public ushort Power { get; }
        
        public double FuelConsumption { get; }
        
        // IBody
        public BodyTypes BodyType { get; }
        
        public byte NumberOfDoors { get; }
        public byte NumberOfSeats { get; }
        public float TrunkVolume { get; }
        
        // Car
        public readonly CarBrands Brand;
        public readonly BmwModels BmwModel;
        public readonly AudiModels AudiModel;
        public readonly double PriceOfCar;
        public readonly float RentPerHour;


        public CarClasses Class { get; }
        public ushort MaxSpeed { get; }
        public ushort YearOfCreation { get; }
        
        
        public Car(CarBrands brand, BmwModels model, double priceOfCar)
        {
            Brand = brand;
            BmwModel = model;
            PriceOfCar = priceOfCar;
            RentPerHour = (float)(priceOfCar / 100000);
            BodyType = GetBodyType(model);
        }
        
        public Car(CarBrands brand, AudiModels model, double priceOfCar)
        {
            Brand = brand;
            AudiModel = model;
            PriceOfCar = priceOfCar;
            RentPerHour = (float)(priceOfCar / 100000);
            BodyType = GetBodyType(model);
        }
        
        private BodyTypes GetBodyType(AudiModels audiModel)
        {
            switch (audiModel)
            {
                case AudiModels.A7:
                    return BodyTypes.Hatchback;
                case AudiModels.A8:
                    return BodyTypes.Sedan;
                case AudiModels.S5:
                    return BodyTypes.Coupe;
                case AudiModels.Sq5:
                    return BodyTypes.Crossover;
            }

            return 0;
        }
        private BodyTypes GetBodyType(BmwModels bmwModel)
        {
            switch (bmwModel)
            {
                case BmwModels.I8:
                    return BodyTypes.Coupe;
                case BmwModels.M5:
                    return BodyTypes.Sedan;
                case BmwModels.X6:
                    return BodyTypes.Crossover;
                case BmwModels.Z4:
                    return BodyTypes.Roadster;
            }
            return 0;
        }
        
        public override string ToString()
        {
            return $"";
        }
    }
}