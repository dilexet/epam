using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.ModelLayer.Models
{
    public class Manager
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Manager surname")]
        public string Surname { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
        
        public Manager()
        {
            Sales = new HashSet<Sale>();
        }
    }
}