using System.ServiceProcess;
using System.Threading;

namespace SalesStatistics.WindowsService
{
    internal class Program
    {
        public static void Main()
        {
            // StatisticsService service = new StatisticsService();
            // service.OnDebug();
            // Thread.Sleep(Timeout.Infinite);
            
            // TODO: Новый баг привет: при запуске службы ошибка 1064,
            // TODO: в журнале событий винды NullReferenceException, при дебаге проблем не обнаружено, +- они где-то в методе ProcessFileHandler
            var servicesToRun = new ServiceBase[]
            {
                new StatisticsService()
            };
            ServiceBase.Run(servicesToRun);

        }
    }
}