using System;

namespace SalesStatistics.WebClient.Models.Filter
{
    public class SalesFilterModel
    {
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}