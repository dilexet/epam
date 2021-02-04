using System.Collections.Generic;
using SalesStatistics.BusinessLogic.DTO;

namespace SalesStatistics.BusinessLogic
{
    public interface IParser
    {
        IEnumerable<SaleDto> FileParse(string filePath);
        
        string NameFileParse(string filePath);
    }
}