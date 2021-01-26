using System.Collections.Generic;
using SalesStatistics.BusinessLogic.DTO;

namespace SalesStatistics.BusinessLogic
{
    public class CsvFileContent
    {
        public IEnumerable<SaleDto> FileContents { get; set; }
        public string Date { get; set; }
        public string ManagerSurname { get; set; }
    }
}