using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.ModelLayer.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Client name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Client surname")]
        public string Surname { get; set; }
        
        public ICollection<Sale> Sales { get; set; }

        public Client()
        {
            Sales = new HashSet<Sale>();
        }
    }
}