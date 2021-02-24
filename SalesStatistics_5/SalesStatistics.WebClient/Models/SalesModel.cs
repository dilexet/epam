using System.Collections.Generic;
using SalesStatistics.WebClient.Models.ViewModels;

namespace SalesStatistics.WebClient.Stuff
{
    public class SalesModel
    {
        public IEnumerable<SaleViewModel> Sales;
        public IEnumerable<ManagerViewModel> Managers;
        public IEnumerable<ProductViewModel> Products;
        public IEnumerable<ClientViewModel> Clients;
    }
}