using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace SalesStatistics.WindowsService
{
    [RunInstaller(true)]
    public partial class StatisticsProcessInstaller : Installer
    {
        public StatisticsProcessInstaller()
        {
            InitializeComponent();
            var serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Manual, 
                ServiceName = "***SalesStatistics***"
            };
            
            var processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}