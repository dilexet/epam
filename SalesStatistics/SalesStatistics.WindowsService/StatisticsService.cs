using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using SalesStatistics.BusinessLogic;
using SalesStatistics.BusinessLogic.Controller;
using SalesStatistics.BusinessLogic.CsvParsing;
using SalesStatistics.BusinessLogic.FileManager;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using Serilog;

namespace SalesStatistics.WindowsService
{
    public partial class StatisticsService: ServiceBase
    {
        private IController _controller;
        
        public StatisticsService()
        {
            InitializeComponent();
            CanStop = true;
        }
        
        protected override void OnStart(string[] args)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var directoryPath = ConfigurationManager.AppSettings["directoryPath"];
                    var filesFilter = ConfigurationManager.AppSettings["filesFilter"];
                    

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.AppSettings()
                        .MinimumLevel.Information()
                        .CreateLogger();

                    var fileHandler = new FileHandler(new Parser());
                    var watcher = new WatcherSourceFileManager(directoryPath, filesFilter, fileHandler);

                    _controller = new SalesController(watcher, new UnitOfWork(new SampleContextFactory()));
                    _controller.Start();
                    Log.Information("Service started successfully");
                });
            }
            catch (NullReferenceException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _controller.Stop();
                _controller?.Dispose();
                Thread.Sleep(1000);
                Log.Information("Service stopped successfully");
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.Message);
            }
        }
    }
}