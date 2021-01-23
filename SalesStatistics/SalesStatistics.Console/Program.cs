using System;
using SalesStatistics.DataAccessLayer.Contexts;
using SalesStatistics.Model.Models;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            
            using (SalesInformationContext db = new SalesInformationContext())
            {
                Manager manager1 = new Manager {Surname = "oooo"};
                Client client1 = new Client {FirstName = "aaaa", Surname = "bbbb"};
                Product product1 = new Product {Name = "iiii"};
                Sale sale1 = new Sale {
                    Client = client1, 
                    Manager = manager1, 
                    Product = product1,
                    Cost = 228,
                    Date = DateTime.Now
                };

                db.Sales.Add(sale1);
                db.SaveChanges();
            }
        }
    }
}