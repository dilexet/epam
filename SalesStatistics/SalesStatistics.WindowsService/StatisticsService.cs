using System.Configuration;
using System.ServiceProcess;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;

namespace SalesStatistics.WindowsService
{
    public partial class StatisticsService: ServiceBase
    {
        private IController _controller;
        public StatisticsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            string filesFilter = ConfigurationManager.AppSettings["filesFilter"];
            
                IFileHandler fileHandler = new FileHandler(new Parser());
                IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

                _controller = new SalesController(watcher, new UnitOfWork());
                _controller.Start();
        }

        protected override void OnStop()
        {
           _controller.Stop();
           _controller.Dispose();
        }
    }
}