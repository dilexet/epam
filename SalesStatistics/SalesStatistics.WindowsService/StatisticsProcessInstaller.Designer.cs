using System.ComponentModel;

namespace SalesStatistics.WindowsService
{
    partial class StatisticsProcessInstaller
    {
        private System.ComponentModel.IContainer components = null;
        
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}