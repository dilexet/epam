using System.Configuration;
using System.IO;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.CsvParsing;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            var filesFilter = ConfigurationManager.AppSettings["filesFilter"];

            DirectoryInfo info = new DirectoryInfo(directoryPath);
            var files = info.GetFiles();

            var parser = new Parser($@"{files[0].DirectoryName}\{files[0].Name}");

            var data = parser.Parse();
            
            using (IController controller = new Controller(directoryPath, filesFilter))
            {
                controller.Start();
                System.Console.ReadKey();
                controller.Stop();
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
                        PurchaseDate = DateTime.Now
                    };
                    var data = ctx.SingleOrDefault(sale => sale.Manager.Surname == "Pp");
                }
            }*/

        }
    }
}