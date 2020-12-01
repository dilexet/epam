using System;
using TaxiFleet.Enums;

namespace TaxiFleet.Taxi
{
    
    internal class CargoTaxi : Car
    {
        public readonly float TrunkCapacity; // объем багажника
        public readonly float CostOfTrip; // стоимость грузоперевозки на 1 час (расчитывается исходя из объема багажинка)
        public readonly float RentPerHour; // Стоимость аренды на 1 час (расчитывается исходя из цены авто)
        public readonly TaxiClasses TaxiClass;

        public CargoTaxi(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber,
            CarColors carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation,
            TaxiClasses taxiClass, float trunkCapacity) :
            base(brand, model, bodyType, carRegistrationNumber, carColor, priceOfCar, fuelConsumption, maxSpeed,
                yearOfCreation)
        {
            TaxiClass = taxiClass;
            TrunkCapacity = trunkCapacity;
            RentPerHour = (float) priceOfCar / 10000;
            CostOfTrip = GetCostOfTrip(trunkCapacity);
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.Write($"Taxi class: {TaxiClass}\n" +
                          $"Rent per hour: {RentPerHour}\n" +
                          $"Trunk capacity: {TrunkCapacity}\n" +
                          $"Cost of trip: {CostOfTrip}\n\n");
        }
        private float GetCostOfTrip(float trunkCapacity)
        {
            if (trunkCapacity <= 10f)
                return 7.75f;
            if (trunkCapacity <= 20f)
                return 9.68f;
            return 5f;
        }
    }
}