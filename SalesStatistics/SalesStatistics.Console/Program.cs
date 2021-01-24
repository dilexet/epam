using SalesStatistics.Controller.FileProcess;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            
            const string path = @"C:\Users\dilexet\Documents\epam\SalesStatistics\SalesStatistics.Controller\Files\Morozov_26012021.csv";

            Parser parser = new Parser(path);
            var item = parser.Parse();

            /*using (var context = new SalesInformationContext())
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
                    // sale1 = null;
                    // sale1 = ctx.Get().FirstOrDefault();
                    // if (sale1 != null) sale1.Cost = 999333;
                    // ctx.Save();
                    // ctx.Add(sale1);
                    // ctx.Save();
                }
            }*/
        }
    }
}