using System.ServiceProcess;

namespace SalesStatistics.WindowsService
{
    internal class Program
    {
        public static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new StatisticsService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}