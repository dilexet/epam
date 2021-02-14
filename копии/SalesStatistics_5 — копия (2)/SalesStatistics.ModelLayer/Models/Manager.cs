using System.Collections.Generic;

namespace SalesStatistics.ModelLayer.Models
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