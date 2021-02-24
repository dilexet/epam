using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.WebClient.Models.ViewModels
{
    public class ClientViewModel
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
    }
}