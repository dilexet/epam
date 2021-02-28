using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesStatistics.WebClient.Models.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase date")]
        public DateTime? Date { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Client name")]
        public string ClientFirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Client surname")]
        public string ClientSurname { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Manager surname")]
        public string ManagerSurname { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Cost")]
        public decimal ProductCost { get; set; }
    }
}