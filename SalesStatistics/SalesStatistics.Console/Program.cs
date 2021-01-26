using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Runtime.Remoting.Contexts;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.DataAccessLayer.UnitOfWork.Operations;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            var filesFilter = ConfigurationManager.AppSettings["filesFilter"];

            using (DbContext context = new SalesInformationContext())
            {
                IRepository<Manager> repositoryManager = new GenericRepository<Manager>(context);
                IRepository<Sale> repositorySale = new GenericRepository<Sale>(context);
            
                IFileHandler fileHandler = new FileHandler(new Parser(), new AddSaleOperation(repositorySale, repositoryManager));
                IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);
            
                using (IController controller = new SalesController(watcher))
                {
                    controller.Start();
                    System.Console.ReadKey();
                    controller.Stop();
                }
            }
            
            
            // const string path = @"C:\Users\dilexet\Documents\epam\SalesStatistics\SalesStatistics.BusinessLogic\Files\Morozov_26012021.csv";
            //
            // Parser parser = new Parser(path);
            // var item = parser.Parse();

            /*using (var context = new SalesInformationContext())
            {
                using (IRepository<Sale> ctx = new GenericRepository<Sale>(context))
                {
                    Manager manager1 = new Manager {Surname = "Pp"};
                    Client client1 = new Client {FirstName = "Mike", Surname = "(("};
                    Product product1 = new Product {Name = "Ooo", Cost = 333};
                    Sale sale1 = new Sale
                    {
                        Client = client1,
                        Manager = manager1,
                        Product = product1,
                        Date = DateTime.Now,
                    };
                    ctx.Add(sale1);
                    ctx.Save();
                }
            }*/

        }
    }
}