using System;
using TaxiFleet.Library.Enums;

namespace TaxiFleet.Library.Models
{
    public class CargoTaxi : CarBase
    {
        public float TrunkCapacity { get; }
        public float CostOfTrip { get; }
        public float RentPerHour { get; }
        public CategoryTaxi CategoryTaxi { get; }

        public CargoTaxi(CarBrand brand, string model, CarBody carBody, string carRegistrationNumber,
            CarColor carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation,
            CategoryTaxi category, float trunkCapacity, float costOfTrip) :
            base(brand, model, carBody, carRegistrationNumber, carColor, priceOfCar, fuelConsumption, maxSpeed,
                yearOfCreation)
        {
            CategoryTaxi = category;
            TrunkCapacity = trunkCapacity;
            RentPerHour = (float) priceOfCar / 10000;
            CostOfTrip = costOfTrip;
        }

        public override string ToString()
        {
            return $"Brand: {Brand}\n" +
                   $"Model: {Model}\n" +
                   $"Body type: {CarBody}\n" +
                   $"Registration number: {CarRegistrationNumber}\n" +
                   $"Color: {CarColor}\n" +
                   $"Price of car: {PriceOfCar}\n" +
                   $"Fuel consumption: {FuelConsumption}\n" +
                   $"Max speed: {MaxSpeed}\n" +
                   $"Year of creation: {YearOfCreation}\n" +
                   $"Taxi class: {CategoryTaxi.TaxiClass}\n" +
                   $"Description: {CategoryTaxi.Description}\n" +
                   $"Rent per hour: {RentPerHour}\n" +
                   $"Trunk capacity: {TrunkCapacity}\n" +
                   $"Cost of trip: {CostOfTrip}\n\n";
        }
    }
}