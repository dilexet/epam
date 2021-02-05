using CsvHelper.Configuration;

namespace SalesStatistics.BusinessLogic.DTO
{
    public sealed class SaleDtoMap: ClassMap<SaleDto>
    {
        public SaleDtoMap()
        {
            Map(saleDto => saleDto.Id).Name("Column1");
            Map(saleDto => saleDto.Date).Name("Column2");
            Map(saleDto => saleDto.Client.Surname).Name("Column3");
            Map(saleDto => saleDto.Client.FirstName).Name("Column4");
            Map(saleDto => saleDto.Product.Name).Name("Column5");
            Map(saleDto => saleDto.Product.Cost).Name("Column6");
        }
    }
}