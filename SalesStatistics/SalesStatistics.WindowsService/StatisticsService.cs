using System;
using System.ServiceProcess;
using System.Threading;
using Serilog;

namespace SalesStatistics.WindowsService
{
    public partial class StatisticsService: ServiceBase
    {
        private  Logger _logger;
        public StatisticsService()
        {
            InitializeComponent();
            CanStop = true;
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        
        protected override void OnStart(string[] args)
        {
            try
            {
                _logger = new Logger();
                Thread loggerThread = new Thread(_logger.Start);
                loggerThread.Start();
                Log.Information("service started successfully");
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
                _logger.Stop();
                Thread.Sleep(1000);
                Log.Information("service stoped successfully");
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.Message);
            }
        }
    }
}