using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.WebClient.Models.ViewModels
{
    public class ManagerViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Manager surname")]
        public string Surname { get; set; }
    }
}