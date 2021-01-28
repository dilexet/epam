using System.IO;
using SalesStatistics.BusinessLogic.FileManager;

namespace SalesStatistics.BusinessLogic
{
    public interface IFileHandler// : IDisposable
    {
        event FileHandler.AddDbHandler AddItemsDbEvent;
        void ProcessFileHandler(object sender, FileSystemEventArgs e);
    }
}