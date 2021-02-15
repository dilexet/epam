using System;
using System.ComponentModel.DataAnnotations;
namespace SalesStatistics.ModelLayer.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
        public int ProductId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase date")]
        public DateTime? Date { get; set; }

        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
    }
}