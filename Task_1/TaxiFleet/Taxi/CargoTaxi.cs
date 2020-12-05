using TaxiFleet.Data.Models;
using TaxiFleet.Enums;

namespace TaxiFleet.Taxi
{
    internal class CargoTaxi : Car
    {
        public readonly float TrunkCapacity; // объем багажника
        public readonly float CostOfTrip; // стоимость грузоперевозки (расчитывается исходя из объема багажинка и при заказе умножается на кол-во кг)
        public readonly float RentPerHour; // Стоимость аренды на 1 час (расчитывается исходя из цены авто)
        public readonly CategoryTaxi CategoryTaxi;

        public CargoTaxi(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber,
            CarColors carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation,
            CategoryTaxi category, float trunkCapacity) :
            base(brand, model, bodyType, carRegistrationNumber, carColor, priceOfCar, fuelConsumption, maxSpeed,
                yearOfCreation)
        {
            CategoryTaxi = category;
            TrunkCapacity = trunkCapacity;
            RentPerHour = (float) priceOfCar / 10000;
            CostOfTrip = GetCostOfTrip(trunkCapacity);
        }
        private float GetCostOfTrip(float trunkCapacity)
        {
            if (trunkCapacity <= 10f)
                return 7.75f;
            if (trunkCapacity <= 20f)
                return 9.68f;
            return 15.3f;
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
                   $"Year of creation: {YearOfCreation}\n" +
                   $"Taxi class: {CategoryTaxi.TaxiClass}\n" +
                   $"Description: {CategoryTaxi.Description}\n" +
                   $"Rent per hour: {RentPerHour}\n" +
                   $"Trunk capacity: {TrunkCapacity}\n" +
                   $"Cost of trip: {CostOfTrip}\n\n";
        }
    }
}