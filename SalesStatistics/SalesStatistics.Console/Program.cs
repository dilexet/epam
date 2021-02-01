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
            // TODO: БАГ: при запуске без отладки, читается только один файл, файлы добавленые после него, не читаются, но при отладке всё работает
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


            
            
         

            /*using (var context = new SalesInformationContext())
            {
                using (IRepository<Product> ctx = new GenericRepository<Product>(context))
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