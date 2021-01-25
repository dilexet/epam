using System.Collections.Generic;

namespace SalesStatistics.BusinessLogic
{
    public class Content
    {
        public IEnumerable<FileContent> FileContents { get; set; }
        public string Date { get; set; }
        public string ManagerSurname { get; set; }
    }
}