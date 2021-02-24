using System.Collections.Generic;

namespace SalesStatistics.DataAccessLayer.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
        
        public Manager()
        {
            Sales = new HashSet<Sale>();
        }
    }
}