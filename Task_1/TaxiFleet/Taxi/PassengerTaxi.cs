using TaxiFleet.Data.Models;
using TaxiFleet.Enums;

namespace TaxiFleet.Taxi
{
    internal class PassengerTaxi: Car
    {
        public readonly float CostOfTrip; // стоимость поездки за 1 км (расчитывается исходя из класса авто)
        public readonly byte NumberOfSeats; // кол-во пасажирских мест(без водителя) (расчитывается исходя из класса авто)
        public readonly float RentPerHour; // Стоимость аренды на 1 час (расчитывается исходя из цены авто)
        public readonly CategoryTaxi CategoryTaxi;
        
        public PassengerTaxi(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber,
            CarColors carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation,
            CategoryTaxi category) :
            base(brand, model, bodyType, carRegistrationNumber, carColor, priceOfCar, fuelConsumption, maxSpeed,
                yearOfCreation)
        {
            CategoryTaxi = category;
            RentPerHour = (float) priceOfCar / 10000;
            NumberOfSeats = GetNumberOfSeats(CategoryTaxi.TaxiClass);
            CostOfTrip = GetCostOfTrip(CategoryTaxi.TaxiClass);
        }

        private float GetCostOfTrip(TaxiClasses taxiClass)
        {
            switch (taxiClass)
            {
                case TaxiClasses.Economy:
                    return 3.24f;
                case TaxiClasses.Comfort:
                    return 3.92f;
                case TaxiClasses.Compactvan:
                    return 5.79f;
                case TaxiClasses.Minivan:
                    return 7.32f;
                case TaxiClasses.Business:
                    return 8.17f;
                default:
                    return 3f;
            }
        }
        private byte GetNumberOfSeats(TaxiClasses taxiClass)
        {
            if (taxiClass == TaxiClasses.Minivan)
                return 7;
            if (taxiClass == TaxiClasses.Compactvan)
                return 6;
            return 4;
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
                   $"Number of seats: {NumberOfSeats}\n" +
                   $"Cost of trip: {CostOfTrip}\n\n";
        }
    }
}