using System;
using System.IO;

namespace SalesStatistics.BusinessLogic
{
    public interface IFileHandler: IDisposable
    {
        void ProcessFileHandler(object sender, FileSystemEventArgs e);
    }
}