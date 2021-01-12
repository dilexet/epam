using System.Collections.Generic;

namespace BillingSystem
{
    public class Report
    {
        private ICollection<Record> _records;

        public Report()
        {
            _records = new List<Record>();
        }

        public void AddRecords(Record record)
        {
            _records.Add(record);
        }
        public ICollection<Record> GerRecords()
        {
            return _records;
        }

        /*public ICollection<Record> SortByDate()
        {
            
        }
        public ICollection<Record> SortByCost()
        {
            
        }
        public ICollection<Record> SortByClient()
        {
            
        }*/
    }
}