using System.ServiceProcess;

namespace SalesStatistics.WindowsService
{
    internal static class Program
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