using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesStatistics.ModelLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Product name")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
        
        public Product()
        {
            Sales = new HashSet<Sale>();
        }
    }
}