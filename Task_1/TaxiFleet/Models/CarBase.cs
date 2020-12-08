using TaxiFleet.Enums;

namespace TaxiFleet.Models
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
        public override int GetHashCode()
        {
            return Model.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return Equals(other as CarBase);
        }

        public bool Equals(CarBase other)
        {
            return other != null &&
                   Brand == other.Brand &&
                   Model == other.Model &&
                   CarBody == other.CarBody &&
                   CarRegistrationNumber == other.CarRegistrationNumber &&
                   CarColor == other.CarColor &&
                   MaxSpeed == other.MaxSpeed &&
                   YearOfCreation == other.YearOfCreation;


        }
    }
}