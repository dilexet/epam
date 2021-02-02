using System.Configuration;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using Serilog;

namespace SalesStatistics.WindowsService
{
    public class Logger
    {
        private IController _controller;
        
        public void Start()
        {
            var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            var filesFilter = ConfigurationManager.AppSettings["filesFilter"];
            var logPath = ConfigurationManager.AppSettings["logPah"];
            var connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logPath)
                .CreateLogger();
            
            IFileHandler fileHandler = new FileHandler(new Parser());
            IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

            _controller = new SalesController(watcher, new UnitOfWork(connectionString));
            _controller.Start();
        }

        public void Stop()
        {
            _controller.Stop();
            _controller.Dispose();
        }
    }
}