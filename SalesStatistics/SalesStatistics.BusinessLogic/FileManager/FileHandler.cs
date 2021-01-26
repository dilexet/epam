using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SalesStatistics.BusinessLogic.DTO;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.BusinessLogic.FileManager
{
    public class FileHandler: IFileHandler
    {
        private IParser _parser;
        private IUnitOfWork _unitOfWork;
        public FileHandler(IParser parser, IUnitOfWork unitOfWork)
        {
            _parser = parser;
            _unitOfWork = unitOfWork;
        }

        public void ProcessFileHandler(object sender, FileSystemEventArgs e)
        {
            Task.Factory.StartNew(() => 
                WriteToDataBase(
                    _parser.FileParse(e.FullPath), 
                    _parser.NameFileParse(e.FullPath))
            );
        }

        private void WriteToDataBase(IEnumerable<SaleDto> sales, string managerSurname)
        {
            var data = CreateModels(sales, managerSurname);
            _unitOfWork.Commit(data);
        }

        private IEnumerable<Sale> CreateModels(IEnumerable<SaleDto> salesDto, string managerSurname)
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

                Manager manager = new Manager();
                manager.Surname = managerSurname;
                sale.Manager = manager;
                
                sales.Add(sale);
            }
            return sales; 
        }
        
        
        private bool _disposed;

        ~FileHandler()
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
        }
    }
}