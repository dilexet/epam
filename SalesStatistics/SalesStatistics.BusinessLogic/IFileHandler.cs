using System.IO;

namespace SalesStatistics.BusinessLogic
{
    public interface IFileHandler
    {
        void ProcessFileHandler(object sender, FileSystemEventArgs e);
    }
}