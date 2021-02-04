using System;
using System.Configuration;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using Serilog;

namespace SalesStatistics.Console
{
    internal static class Program
    {
        public static void Main()
        {
            var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            var filesFilter = ConfigurationManager.AppSettings["filesFilter"];
            var logPath = ConfigurationManager.AppSettings["logPath"];
            
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(logPath)
                .CreateLogger();

            try
            {
                IFileHandler fileHandler = new FileHandler(new Parser());
                IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

                using (IController controller =
                    new SalesController(watcher, new UnitOfWork(new SampleContextFactory())))
                {
                    controller.Start();
                    System.Console.ReadKey();
                    Log.CloseAndFlush();
                    controller.Stop();
                }
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.Message);
            }


            /*var connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            using (var context = new SalesInformationContext(connectionString))
            {
                IRepository<Manager> ctxM = new GenericRepository<Manager>(context);
                IRepository<Product> ctxP = new GenericRepository<Product>(context);
                IRepository<Client> ctxC = new GenericRepository<Client>(context);
                IRepository<Sale> ctxS = new GenericRepository<Sale>(context);

                var managers = ctxM.Get();
                var products = ctxP.Get();
                var clients = ctxC.Get();
                var sales = ctxS.Get();
                
                foreach (var sale in managers)
                {
                    ctxM.Remove(sale);
                }
                foreach (var sale in products)
                {
                    ctxP.Remove(sale);
                }
                foreach (var sale in clients)
                {
                    ctxC.Remove(sale);
                }
                foreach (var sale in sales)
                {
                    ctxS.Remove(sale);
                }
                
                context.SaveChanges();
            }*/
            
        }
    }
}