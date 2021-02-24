using System;
using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.WebClient.Models.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase date")]
        public DateTime? Date { get; set; }

        public ClientViewModel Client { get; set; }
        public ManagerViewModel Manager { get; set; }
        public ProductViewModel Product { get; set; }
    }
}