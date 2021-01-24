using System;
using System.Linq;
using SalesStatistics.Controller.FileProcess;
using SalesStatistics.DataAccessLayer.EntityFraimworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            
            // const string path = @"C:\Users\dilexet\Documents\epam\SalesStatistics\SalesStatistics.Controller\Files\Morozov_26012021.csv";
            //
            // Parser parser = new Parser(path);
            // var item = parser.Parse();

            using (var context = new SalesInformationContext())
            {
                using (var ctx = new GenericRepository<Sale>(context))
                {
                    Manager manager1 = new Manager {Surname = "Gg"};
                    Client client1 = new Client {FirstName = "Nick", Surname = ")))"};
                    Product product1 = new Product {Name = "qqq", Cost = 666};
                    Sale sale1 = new Sale
                    {
                        Client = client1,
                        Manager = manager1,
                        Product = product1,
                        Date = DateTime.Now,
                        PurchaseDate = DateTime.Now
                    };
                    ctx.Add(sale1);
                    ctx.Save();
                }
            }
        }
    }
}