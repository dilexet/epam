namespace SalesStatistics.BusinessLogic.DTO
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string Date { get; set; }

        public ClientDto Client { get; set; }
        public ProductDto Product { get; set; }
    }
}