using System;
using System.Collections.Generic;
using System.Globalization;
using SalesStatistics.BusinessLogic.DTO;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.ModelLayer.Models;
using Serilog;

namespace SalesStatistics.BusinessLogic.Controller
{
    public sealed class SalesController: IController
    {
        private readonly IDirectoryWatcher _watcher;
        private readonly IUnitOfWork _unitOfWork;
        public SalesController(IDirectoryWatcher watcher, IUnitOfWork unitOfWork)
        {
            _watcher = watcher;
            _unitOfWork = unitOfWork;
        }

        public void Start()
        {
            _watcher.FileHandler.MakeManagerEvent += MakeManagerHandler;
            _watcher.Start();
        }
        
        public void Stop()
        {
            _watcher.FileHandler.MakeManagerEvent -= MakeManagerHandler;
            _watcher.Stop();
        }

        private void MakeManagerHandler(IEnumerable<SaleDto> salesDto, ManagerDto managerDto)
        {
            Manager manager = new Manager();
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

                    Client client = new Client {Surname = item.Client.Surname, FirstName = item.Client.FirstName};
                    sale.Client = client;

                    Product product = new Product {Name = item.Product.Name, Cost = Convert.ToDecimal(item.Product.Cost)};
                    sale.Product = product;

                    sales.Add(sale);
                }

                manager.Surname = managerDto.Surname;
                manager.Sales = sales;
                _unitOfWork.Add(manager);
            }
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Log.Error("{Message}", e.Message);
            }
        }
        
        private bool _disposed;

        ~SalesController()
        {
            Dispose();
        }
        
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _watcher.Dispose();
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