using System.ComponentModel;
using System.Configuration.Install;

namespace SalesStatistics.WindowsService
{
    [RunInstaller(true)]
    public partial class StatisticsProcessInstaller : Installer
    {
        public StatisticsProcessInstaller()
        {
            InitializeComponent();
        }
    }
}