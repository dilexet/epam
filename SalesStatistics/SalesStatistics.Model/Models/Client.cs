using System.Collections.Generic;

namespace SalesStatistics.Model.Models
{
    public sealed class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        public ICollection<Sale> Sales { get; set; }

        public Client()
        {
            Sales = new HashSet<Sale>();
        }
    }
}