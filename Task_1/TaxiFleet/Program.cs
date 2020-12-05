using TaxiFleet.Taxi;

namespace TaxiFleet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            TaxiPark taxiPark = new TaxiPark();
            taxiPark.Print();
            //taxiPark.PrintInfo();
            // taxiPark.TotalRevenueOfTaxiPark();
            // Customer person1 = new Customer("Maksim");
            // person1.TaxiOrdering(taxiPark);
            // taxiPark.SortingByFuelConsumption();
            // var car = taxiPark.SelectSpeedTaxi();
            
            /*
            foreach (var item in car)
            {
                item.PrintInfo();
            }
            */
            
        }
    }
}