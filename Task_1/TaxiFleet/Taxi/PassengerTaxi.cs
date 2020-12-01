﻿using System;
using TaxiFleet.Enums;

namespace TaxiFleet.Taxi
{
    internal class PassengerTaxi: Car
    {
        public readonly float CostOfTrip; // стоимость поездки за 1 км (расчитывается исходя из класса авто)
        public readonly byte NumberOfSeats; // кол-во пасажирских мест(без водителя) (расчитывается исходя из класса авто)
        public readonly float RentPerHour; // Стоимость аренды на 1 час (расчитывается исходя из цены авто)
        public readonly TaxiClasses TaxiClass;

        public PassengerTaxi(CarBrands brand, string model, BodyTypes bodyType, string carRegistrationNumber,
            CarColors carColor, double priceOfCar, float fuelConsumption, ushort maxSpeed, ushort yearOfCreation,
            TaxiClasses taxiClass) :
            base(brand, model, bodyType, carRegistrationNumber, carColor, priceOfCar, fuelConsumption, maxSpeed,
                yearOfCreation)
        {
            TaxiClass = taxiClass;
            RentPerHour = (float) priceOfCar / 10000;
            NumberOfSeats = GetNumberOfSeats(taxiClass);
            CostOfTrip = GetCostOfTrip(taxiClass);
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.Write($"Taxi class: {TaxiClass}\n" +
                          $"Rent per hour: {RentPerHour}\n" +
                          $"Number of seats: {NumberOfSeats}\n" +
                          $"Cost of trip: {CostOfTrip}\n\n");
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
    }
}