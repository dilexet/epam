using System.Configuration;
using System.ServiceProcess;
using SalesStatistics.BusinessLogic;

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

            _controller = new Controller(directoryPath, filesFilter);
            _controller.Start();
        }

        protected override void OnStop()
        {
           _controller.Start();
           _controller.Dispose();
        }
    }
}