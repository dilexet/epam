using System;
using SalesStatistics.DataAccessLayer.EntityFraimworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            using (var context = new SalesInformationContext())
            {
                using (var ctx = new GenericRepository<Sale>(context))
                {
                    Manager manager1 = new Manager {Surname = "Gg"};
                    Client client1 = new Client {FirstName = "Nick", Surname = ")))"};
                    Product product1 = new Product {Name = "qqq"};
                    Sale sale1 = new Sale
                    {
                        Client = client1,
                        Manager = manager1,
                        Product = product1,
                        Cost = 666,
                        Date = DateTime.Now
                    };
                    // ctx.Add(sale1);
                    // ctx.Save();
                }
            }
        }
    }
}