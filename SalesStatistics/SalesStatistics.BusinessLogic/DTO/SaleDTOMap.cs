using CsvHelper.Configuration;

namespace SalesStatistics.BusinessLogic.DTO
{
    public sealed class SaleDtoMap: ClassMap<SaleDto>
    {
        public SaleDtoMap()
        {
            Map(m => m.Id).Name("Column1");
            Map(m => m.Date).Name("Column2");
            Map(m => m.ClientSurname).Name("Column3");
            Map(m => m.ClientFirstName).Name("Column4");
            Map(m => m.ProductName).Name("Column5");
            Map(m => m.ProductCost).Name("Column6");
        }
    }
}