using System;
using System.Collections.Generic;
using System.IO;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.BusinessLogic
{
    public interface IFileHandler
    {
        event Action<IEnumerable<Sale>, Manager> AddItemsDbEvent;
        
        void ProcessFileHandler(object sender, FileSystemEventArgs e);
    }
}