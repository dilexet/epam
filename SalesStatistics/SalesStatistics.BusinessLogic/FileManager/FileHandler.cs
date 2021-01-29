using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.BusinessLogic.FileManager
{

    public class FileHandler : IFileHandler
    {
        private IParser _parser;

        public delegate void AddDbHandler(IEnumerable<Sale> sales, Manager manager);

        public event AddDbHandler AddItemsDbEvent;

        public FileHandler(IParser parser)
        {
            _parser = parser;
        }

        public void ProcessFileHandler(object sender, FileSystemEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    WriteToDataBase(
                        _parser.FileParse(e.FullPath),
                        _parser.NameFileParse(e.FullPath));
                }
                catch (HeaderValidationException)
                {
                    throw new Exception($"{e.Name} First Line Must Match Template => Date | Client | Product | Cost");
                }
                catch (FormatException exception)
                {
                    throw new FormatException($"{e.Name} {exception.Message}");
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentNullException(nameof(e));
                }
                catch (Exception)
                {
                    throw new Exception($"{e.Name} Information is not Added to Database");
                }
            });
            
        }

        private void WriteToDataBase(IEnumerable<SaleDto> sales, string managerSurname)
        {
            var data = CreateModels(sales);
            AddItemsDbEvent?.Invoke(data, new Manager {Surname = managerSurname});
        }

        private IEnumerable<Sale> CreateModels(IEnumerable<SaleDto> salesDto)
        {
            try
            {
                ICollection<Sale> sales = new List<Sale>();
                foreach (var item in salesDto)
                {
                    Sale sale = new Sale();

                    sale.Date = Convert.ToDateTime(item.Date);

                    Client client = new Client();
                    client.Surname = item.ClientSurname;
                    client.FirstName = item.ClientFirstName;
                    sale.Client = client;

                    Product product = new Product();
                    product.Name = item.ProductName;
                    product.Cost = Convert.ToDecimal(item.ProductCost);
                    sale.Product = product;

                    sales.Add(sale);
                }

                return sales;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentNullException(salesDto.ToString());
            }
        }
    }
}
// private bool _disposed;

        /*~FileHandler()
        {
            Dispose();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }*/
    