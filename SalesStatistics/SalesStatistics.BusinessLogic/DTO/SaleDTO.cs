namespace SalesStatistics.BusinessLogic.DTO
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string PurchaseDate { get; set; }
        public string ClientSurname { get; set; }
        public string ClientFirstName { get; set; }
        public string ProductName { get; set; }
        public string ProductCost { get; set; }
    }
}