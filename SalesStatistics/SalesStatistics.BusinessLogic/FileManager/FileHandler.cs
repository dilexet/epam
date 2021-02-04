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
        private readonly IParser _parser;
        
        public event Action<IEnumerable<Sale>, Manager> AddItemsDbEvent;

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
                    Log.Error("{Name} First Line Must Match Template => ID | Date | Client | Product | Cost", e.Name);
                }
                catch (TypeLoadException exception)
                {
                    Log.Error("{Message}", exception.Message);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Log.Error("Invalid file name: {Message}", exception.Message);
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
            try
            {
                var data = CreateModels(sales);
                AddItemsDbEvent?.Invoke(data, new Manager {Surname = managerSurname});
            }
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
        }

        private IEnumerable<Sale> CreateModels(IEnumerable<SaleDto> salesDto)
        {
            try
            {
                ICollection<Sale> sales = new List<Sale>();
                foreach (var item in salesDto)
                {
                    Sale sale = new Sale
                    {
                        Date = DateTime.ParseExact(
                            item.Date,
                            "dd.MM.yyyy",
                            CultureInfo.InvariantCulture).Date
                    };

                    Client client = new Client {Surname = item.ClientSurname, FirstName = item.ClientFirstName};
                    sale.Client = client;

                    Product product = new Product {Name = item.ProductName, Cost = Convert.ToDecimal(item.ProductCost)};
                    sale.Product = product;

                    sales.Add(sale);
                }

                return sales;
            }
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Log.Error("{Message}", e.Message);
            }

            return null;
        }
    }
}    