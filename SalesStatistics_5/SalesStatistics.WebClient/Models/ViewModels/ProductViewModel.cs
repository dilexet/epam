using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesStatistics.WebClient.Models.ViewModels
{
    public class ProductViewModel
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
    }
}