using System.Configuration;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;

namespace SalesStatistics.Console
{
    internal class Program
    {
        public static void Main()
        {
            var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            var filesFilter = ConfigurationManager.AppSettings["filesFilter"];


            IFileHandler fileHandler = new FileHandler(new Parser());
            IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

            using (IController controller = new SalesController(watcher, new UnitOfWork()))
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
                using (IRepository<Client> ctx = new GenericRepository<Client>(context))
                {
                    var sales = ctx.Get();

                    foreach (var sale in sales)
                    {
                        ctx.Remove(sale);
                    }
                    ctx.Save();
                }
            }*/

        }
    }
}