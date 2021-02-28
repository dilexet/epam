using System.Collections.Generic;
using System.Linq;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Models.Filter;

namespace SalesStatistics.WebClient.Services
{
    public static class DataFilter
    {
        public static IEnumerable<Sale> Filter(SalesFilterModel salesFilterModel, IEnumerable<Sale> sales)
        {
            if (!string.IsNullOrEmpty(salesFilterModel.ClientName)) 
            {
                sales = sales.Where(s =>
                    s.Client.Surname.ToUpper().Contains(salesFilterModel.ClientName.ToUpper()) ||
                    s.Client.FirstName.ToUpper().Contains(salesFilterModel.ClientName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(salesFilterModel.ProductName))
            {
                sales = sales.Where(s => s.Product.Name.ToUpper().Contains(salesFilterModel.ProductName.ToUpper()));
            }

            if (salesFilterModel.DateStart != null && salesFilterModel.DateEnd != null && salesFilterModel.DateStart < salesFilterModel.DateEnd)
            {
                sales = sales.Where(s =>
                    s.Date >= salesFilterModel.DateStart && s.Date <= salesFilterModel.DateEnd);
            }
            
            return sales.ToList();
        }
    }
}