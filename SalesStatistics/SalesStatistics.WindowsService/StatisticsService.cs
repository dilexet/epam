using System.Configuration;
using System.Data.Entity;
using System.ServiceProcess;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.DataAccessLayer.UnitOfWork.Operations;
using SalesStatistics.ModelLayer.Models;

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

            
            using (DbContext context = new SalesInformationContext())
            {
                IRepository<Manager> repositoryManager = new GenericRepository<Manager>(context);
                IRepository<Sale> repositorySale = new GenericRepository<Sale>(context);
            
                IFileHandler fileHandler = new FileHandler(new Parser(), new AddSaleOperation(repositorySale, repositoryManager));
                IDirectoryWatcher watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

                _controller = new SalesController(watcher);
                _controller.Start();
            }
        }

        protected override void OnStop()
        {
           _controller.Stop();
           _controller.Dispose();
        }
    }
}