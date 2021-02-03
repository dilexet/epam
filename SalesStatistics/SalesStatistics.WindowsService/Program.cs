using System.ServiceProcess;

namespace SalesStatistics.WindowsService
{
    internal class Program
    {
        public static void Main()
        {
            // StatisticsService service = new StatisticsService();
            // service.OnDebug();
            // Thread.Sleep(Timeout.Infinite);
            
            var servicesToRun = new ServiceBase[]
            {
                new StatisticsService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}