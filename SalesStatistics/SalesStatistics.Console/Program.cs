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

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .MinimumLevel.Information()
                .CreateLogger();
            try
            {
                var fileHandler = new FileHandler(new Parser());
                var watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

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
        }
    }
}