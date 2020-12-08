using System;
using System.Collections.Generic;
using TaxiFleet.Models;

namespace TaxiFleet
{
    public class Info
    {
        public static void PrintInfo(IEnumerable<CarBase> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}