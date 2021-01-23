namespace SalesStatistics.ModelLayer.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
        public int ProductId { get; set; }
        
        public System.DateTime Date { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
    }
}