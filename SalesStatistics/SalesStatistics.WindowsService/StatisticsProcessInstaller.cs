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
            var serviceInstaller = new ServiceInstaller();
            var processInstaller = new ServiceProcessInstaller();
            
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "***SalesStatistics***";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}