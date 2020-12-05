using System;
using System.Collections.Generic;
using TaxiFleet.Data.Models;

namespace TaxiFleet
{
    internal class Info
    {
        public static void PrintInfo(IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}