using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;
using SalesStatistics.ModelLayer.Models;
using Serilog;

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
                    while (IsFileLocked(new FileInfo(e.FullPath)))
                    {
                        
                    }
                    
                    WriteToDataBase(
                        _parser.FileParse(e.FullPath),
                        _parser.NameFileParse(e.FullPath));
                }
                catch (HeaderValidationException)
                {
                    Log.Error("{Name} First Line Must Match Template => Date | Client | Product | Cost", e.Name);
                }
                catch (FormatException exception)
                {
                    Log.Error("{Name} {Message}", e.Name, exception.Message);
                }
            });
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
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

                    sale.Date = DateTime.ParseExact(
                        item.Date, 
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture);

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