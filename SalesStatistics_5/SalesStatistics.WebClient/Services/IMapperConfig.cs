using System.Collections.Generic;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Models.ViewModels;

namespace SalesStatistics.WebClient.Services
{
    public interface IMapperConfig
    {
        IEnumerable<SaleViewModel> MapConfig(IEnumerable<Sale> sales);
        SaleViewModel MapConfig(Sale sale);
        Sale MapConfig(SaleViewModel saleView);
    }
}